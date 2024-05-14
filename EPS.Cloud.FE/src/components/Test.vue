<template>
  <div style="width: 300px; height: 180px"></div>

  <div class="map" id="map" style="width: 100%; height: 100%"></div>
</template>

<script>
import {
  GoogleMap,
  Marker,
  MarkerCluster,
  InfoWindow,
  CustomMarker,
} from "vue3-google-map";
import Stars from "@/components/icons/Stars.vue";
import Clock from "@/components/icons/Clock.vue";
import { Loader } from "@googlemaps/js-api-loader";
export default {
  components: {
    GoogleMap,
    Marker,
    MarkerCluster,
    InfoWindow,
    CustomMarker,
    Stars,
    Clock,
  },
  props: {
    locations: {
      type: Array,
      default: [],
    },
    center: {
      type: Object,
      default: {},
    },
  },
  data() {
    return {
      selectedLocation: null,
      infoWindowOpened: false,
      infoWindowOptions: {
        pixelOffset: { width: 0, height: -40 },
      },
      loader: new Loader({
        apiKey: "AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg",
        version: "weekly",
        libraries: ["places"],
      }),
      map: null,
    };
  },
  async mounted() {
    this.initMap();
  },

  methods: {
    showInfoWindow(location) {
      this.selectedLocation = location;
      this.infoWindowOpened = true;
    },
    handleClickOutside() {
      alert(123);
    },
    handleMouseOver() {
      const me = this;
      console.log(123);
    },
    handleMouseOut() {
      const me = this;
      console.log(123);
    },
    async initMap() {
      try {
        const google = await this.loader.load();
        const directionsRenderer = new google.maps.DirectionsRenderer();
        const directionsService = new google.maps.DirectionsService();
        this.map = new google.maps.Map(this.$el, {
          center: { lat: 21.035115, lng: 105.7864797 },
          zoom: 8,
        });

        directionsRenderer.setMap(this.map);
        this.calculateAndDisplayRoute(directionsService, directionsRenderer);
      } catch (error) {
        console.log(error);
      }
    },
    calculateAndDisplayRoute(directionsService, directionsRenderer) {
      directionsService
        .route({
          origin: { lat: 21.035115, lng: 105.7864797 },
          destination: { lat: 21.0591763, lng: 105.8018828 },
          travelMode: google.maps.TravelMode.DRIVING,
        })
        .then((response) => {
          directionsRenderer.setDirections(response);
        })
        .catch((e) =>
          window.alert("Directions request failed due to " + e.status)
        );
    },
  },
};
</script>
<style lang="scss">
// @import "./google-map.scss";</style>
