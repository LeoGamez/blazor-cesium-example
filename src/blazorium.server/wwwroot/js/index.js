let viewer;

function helloWorld() {
    alert("helloWorld");
}

function createViewer() {
    Cesium.Ion.defaultAccessToken = 'Insert valid token';

    // Initialize the Cesium Viewer in the HTML element with the `cesiumContainer` ID.
    viewer = new Cesium.Viewer('cesium-map-container', {
        terrainProvider: Cesium.createWorldTerrain()
    });
    viewer.scene.globe.depthTestAgainstTerrain = false;

}

function addPoint(weatherPoint) {

    viewer.entities.add({
        position: Cesium.Cartesian3.fromDegrees(weatherPoint.longitude, weatherPoint.latitude),
        point: {
            pixelSize: 8,
            color: Cesium.Color.BLUE,
            outlineColor: Cesium.Color.WHITE,
            outlineWidth: 2
        },
        label: {
            text: weatherPoint.temperatureC+'° C',
            font: '12px Verdana',
            showBackground: true,
            horizontalOrigin: Cesium.HorizontalOrigin.LEFT,
            verticalOrigin: Cesium.VerticalOrigin.TOP
        }
    });
}

function removeAllEntities(weatherPoint) {
    viewer.entities.removeAll();
}
