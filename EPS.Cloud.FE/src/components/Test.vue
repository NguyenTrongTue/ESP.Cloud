<template>
  <div style="width: 300px; height: 180px"></div>
  <!-- <GoogleMap
    api-key="AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg"
    style="width: 100%; height: 100%"
    :center="center"
    :zoom="13"
  >
    <Marker
      v-for="(location, i) in locations"
      :options="{
        position: { lat: location.latitude, lng: location.longitude },
      }"
      :key="i"
    >
      <div class="custom-marker">
        <div class="custom-marker__img"><Stars /></div>
        <div>{{ location.rating }}</div>
        <span>({{ location.numberOfReviews }})</span>
      </div>
      <InfoWindow ref="info-window">
        <div class="info-window">
          <div class="info-window__top">
            <img
            s
              src="https://storage.googleapis.com/rp-production-public-content/3z624a2usqnwc07nb8ni93deixc7"
              alt="Ảnh của gara"
            />
          </div>
          <div class="info-window__bottom">
            <div class="gara-name__wrapper">
              <div class="gara-name">{{ location.garage_name }}</div>
              <div class="rating">
                <Stars />
                <div>{{ location.total_rating }}</div>
                <span>(40)</span>
              </div>
            </div>
            <div class="address">
              <img
                src="https://www.gstatic.com/images/icons/material/system_gm/1x/place_gm_blue_24dp.png"
                alt="Ảnh của gara"
              />

              <span class="address-text">{{ location.address }}</span>
              <span> ({{ location.distance.toFixed(2) }} km)</span>
            </div>
            <div class="time-open">
              <Clock style="color: rgb(0, 60, 255)" />
              <span>{{ location.open_time }}</span>
            </div>
            <div class="facebook">
              <img
                src="https://www.gstatic.com/images/icons/material/system_gm/1x/public_gm_blue_24dp.png"
                alt="Ảnh của gara"
              />
              <a :href="location.garage_website" target="_blank"
                >facebook.com</a
              >
            </div>
            <div class="phone">
              <img
                src="https://www.gstatic.com/images/icons/material/system_gm/1x/phone_gm_blue_24dp.png"
                alt="Ảnh của gara"
              />

              <span>{{ location.phone }}</span>
            </div>
            <button class="booking">Đặt lịch</button>
          </div>
        </div>
      </InfoWindow>
    </Marker>
    <CustomMarker
      :options="{
        position: { lat: center.lat, lng: center.lng },
      }"
      ><div class="current-location"></div
    ></CustomMarker>
  </GoogleMap> -->

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
// @import "./google-map.scss";
</style>
