<style>
@import "./google-map.scss";
</style>

<template>
  <div class="map" id="map" style="width: 100%; height: 100%"></div>
</template>

<script>
import { Loader } from "@googlemaps/js-api-loader";
export default {
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
      loader: new Loader({
        apiKey: 'AIzaSyAQ2Jcz1gngtqWN1w-mtQ2ja1i49EerB50', //"AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg",
        version: "weekly",
        libraries: ["places"],
      }),
      map: null,
      markers: [],
      google: null,
      infowindow: null,
      location: null,
      locationsList: this.locations,
      directionsRenderer: null,
      directionsService: null,
    };
  },
  mounted() {
    this.initMap();
  },
  watch: {
    locations: {
      handler(newLocations) {
        if (newLocations && newLocations.length > 0) {
          this.locationsList = newLocations;
          this.handleViewLocations();
        }
      },
      deep: true,
    },
  },

  methods: {
    async initMap() {
      try {
        const google = await this.loader.load();
        this.google = google;
        this.directionsRenderer = new google.maps.DirectionsRenderer({
          draggable: true,
          map,
          polylineOptions: {
            strokeColor: "#034efc",
            strokeWeight: 8,
          },
        });
        this.directionsService = new google.maps.DirectionsService();

        const { AdvancedMarkerElement } = await google.maps.importLibrary(
          "marker"
        );

        if (!this.center.lat || !this.center.lng) {
          let value = this.$common.cache.getCache("coords");
          if (value) {
            this.center = {
              lat: value.latitude,
              lng: value.longitude,
            };
          }
        }
        this.map = new google.maps.Map(this.$el, {
          center: { lat: this.center.lat, lng: this.center.lng },
          zoom: 12,
        });

        this.directionsRenderer.setMap(this.map);

        const infowindow = new this.google.maps.InfoWindow();
        this.infowindow = infowindow;
        this.handleViewLocations();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Handle viewing locations on the map.
     */
    handleViewLocations() {
      try {
        this.locations.forEach((location) => {
          let marker = new this.google.maps.Marker({
            map: this.map,
            position: { lat: location.latitude, lng: location.longitude },
          });

          marker.addListener("click", () =>
            this.hanleShowInfoWindow(location, marker)
          );
          this.markers.push(marker);
        });
      } catch (e) {
        console.log(e);
      }
    },
    calculateAndDisplayRoute(dest) {
      this.directionsService
        .route({
          origin: { lat: this.center.lat, lng: this.center.lng },
          destination: { lat: dest.latitude, lng: dest.longitude },
          travelMode: google.maps.TravelMode.DRIVING,
          avoidTolls: true,
        })
        .then((response) => {
          this.directionsRenderer.setDirections(response);
        })
        .catch((e) =>
          console.log("Directions request failed due to " + e.status)
        );
    },
    generateInfoWindowContent() {
      return `<div ref="info-window">
        <div class="info-window">
          <div class="info-window__top">
            <img
              style="width: 300px; height: 180px"
              class="info-window__top-img"
              src="${this.location?.image}"
              alt="Ảnh của gara"
            />
          </div>
          <div class="info-window__bottom">
            <div class="gara-name__wrapper">
              <div class="gara-name" style="font-size: 14px;">${this.location.garage_name
        }</div>
              <div class="rating">
                  <svg
                  style="margin-bottom:3px"
                  xmlns="http://www.w3.org/2000/svg"
                  width="13"
                  height="13"
                  fill="#E98B2F"
                  viewBox="0 0 256 256"
                  class="ng-mr4 ng-ml4"
                >
                  <path
                    d="M234.5,114.38l-45.1,39.36,13.51,58.6a16,16,0,0,1-23.84,17.34l-51.11-31-51,31a16,16,0,0,1-23.84-17.34L66.61,153.8,21.5,114.38a16,16,0,0,1,9.11-28.06l59.46-5.15,23.21-55.36a15.95,15.95,0,0,1,29.44,0h0L166,81.17l59.44,5.15a16,16,0,0,1,9.11,28.06Z"
                  ></path>
                </svg>
                <span>${this.location.total_rating}</span>
                <span>(40)</span>
              </div>
            </div>
            <div class="address">
              <img
                src="https://www.gstatic.com/images/icons/material/system_gm/1x/place_gm_blue_24dp.png"
                alt="Ảnh của gara"
              />

              <span class="address-text">${this.location.address}</span>
              <span class="address-distance"> (${this.location.distance.toFixed(
          2
        )} km)</span>
            </div>
            <div class="time-open">

                <svg
                style="color: rgb(0, 60, 255)"
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            fill="currentColor"
            viewBox="0 0 256 256"
          >
            <path
              d="M128,24A104,104,0,1,0,232,128,104.11,104.11,0,0,0,128,24Zm0,192a88,88,0,1,1,88-88A88.1,88.1,0,0,1,128,216Zm64-88a8,8,0,0,1-8,8H128a8,8,0,0,1-8-8V72a8,8,0,0,1,16,0v48h48A8,8,0,0,1,192,128Z"
            ></path>
          </svg>
              <span>${this.location.open_time}</span>
            </div>
            <div class="facebook">
              <img
                src="https://www.gstatic.com/images/icons/material/system_gm/1x/public_gm_blue_24dp.png"
                alt="Ảnh của gara"
              />
              <a href="${this.location.garage_website}" target="_blank"
                >facebook.com</a
              >
            </div>
            <div class="phone">
              <img
                src="https://www.gstatic.com/images/icons/material/system_gm/1x/phone_gm_blue_24dp.png"
                alt="Ảnh của gara"
              />

              <span>${this.location.phone}</span>
            </div>
          </div>
        </div>
      </div>`;
    },
    generateMarkerContent(location) {
      return `<div class="custom-marker">
        <div class="custom-marker__img"> <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="13"
                  height="13"
                  fill="#E98B2F"
                  viewBox="0 0 256 256"
                  class="ng-mr4 ng-ml4"
                >
                  <path
                    d="M234.5,114.38l-45.1,39.36,13.51,58.6a16,16,0,0,1-23.84,17.34l-51.11-31-51,31a16,16,0,0,1-23.84-17.34L66.61,153.8,21.5,114.38a16,16,0,0,1,9.11-28.06l59.46-5.15,23.21-55.36a15.95,15.95,0,0,1,29.44,0h0L166,81.17l59.44,5.15a16,16,0,0,1,9.11,28.06Z"
                  ></path>
                </svg></div>
        <div> ${location.rating}</div>
        <span>( ${location.numberOfReviews})</span>
      </div>`;
    },
    hanleShowInfoWindow(location, marker) {
      this.location = { ...location };
      this.infowindow.setContent(this.generateInfoWindowContent());
      this.infowindow.open({
        anchor: marker,
        map,
      });
    },
  },
};
</script>