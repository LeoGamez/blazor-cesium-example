let viewer;

function helloWorld() {
    alert("helloWorld");
}

function createViewer() {
    Cesium.Ion.defaultAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwOTQwMTMxNS01MWQxLTRmM2YtODc1NC1jMTFjMWE4MDBjMDgiLCJpZCI6MTAzODMyLCJpYXQiOjE2NTk3MzI5NjB9.4MigYSwwMilQDSf8_KMwPvyrf3nHY9VrJ1hyWAHw4yc';

    // Initialize the Cesium Viewer in the HTML element with the `cesiumContainer` ID.
    viewer = new Cesium.Viewer('cesium-map-container', {
        terrainProvider: Cesium.createWorldTerrain()
    });
}

function addPoint(weatherPoint) {

    viewer.entities.add({
        position: Cesium.Cartesian3.fromDegrees(weatherPoint.longitude, weatherPoint.latitude),
        point: {
            pixelSize: 5,
            color: Cesium.Color.RED,
            outlineColor: Cesium.Color.WHITE,
            outlineWidth: 2
        },
        label: {
            text: weatherPoint.temperatureC+'° C',
            font: '14px monospace',
            showBackground: true,
            horizontalOrigin: Cesium.HorizontalOrigin.LEFT,
            verticalOrigin: Cesium.VerticalOrigin.TOP,
        }
    });
}

function removeAllEntities(weatherPoint) {
    viewer.entities.removeAll();
}