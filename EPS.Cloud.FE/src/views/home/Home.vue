<template>
  <div class="home">
    <SideBar
      :locationProps="locations"
      @sortBy="handleSortBy"
      @typeCars="handleTypeCars"
      @openTime="handleOpenTime"
      @services="handleServices"
      @showPostion="handleShowPostion"
    />
    <GoogleMap :center="center" :locations="locations" ref="google-map" />
    <!-- <Test /> -->
  </div>
</template>

<script>
import GoogleMap from "@/components/map/GoogleMap.vue";
import SideBar from "@/components/sidebar/SideBar.vue";
import Test from "@/components/Test.vue";
import GarageAPI from "@/apis/GarageAPI";
import { useGeolocation } from "@vueuse/core";
const { coords } = useGeolocation();
export default {
  components: {
    GoogleMap,
    SideBar,
    Test,
  },
  data() {
    return {
      center: {},
      locations: [],
      coords,
      searchObject: {
        Coordinates: {
          latitude: 0,
          longitude: 0,
        },
        SortBy: "",
        ListServiceNames: [],
        CarType: "",
        TimeOpen: 1,
        take: 70,
        skip: 0,
      },
    };
  },

  beforeMount() {
    /**
     * Đăng ký sự kiện đóng form
     */
    this.$emitter.on("directing", (index) => {
      this.handleDirecting(index);
    });
  },
  async mounted() {
    let value = this.$common.cache.getCache("coords");
    if (value) {
      this.center = {
        lat: value.latitude,
        lng: value.longitude,
      };
      this.asignCenter();
    }
  },
  watch: {
    "coords.latitude"(newValue) {
      const me = this;
      if (newValue) {
        this.center.lat = +newValue;
        if (newValue && this.center.lng) {
          this.$common.cache.setCache("coords", {
            latitude: this.center.lat,
            longitude: this.center.lng,
          });
        }
        me.asignCenter();
      }
    },
    "coords.longitude"(newValue) {
      const me = this;
      if (newValue) {
        this.center.lng = +newValue;
        me.asignCenter();
        this.$common.cache.setCache("coords", {
          latitude: this.center.lat,
          longitude: this.center.lng,
        });
      }
    },
    searchObject: {
      handler(newObject) {
        const me = this;
        me.searchData(newObject);
      },
      deep: true,
    },
  },
  computed: {
    googleMapRef() {
      return this.$refs["google-map"];
    },
  },
  methods: {
    handleSortBy(value) {
      this.searchObject.SortBy = value;
    },
    handleTypeCars(value) {
      this.searchObject.CarType = value;
    },
    handleOpenTime(value) {
      this.searchObject.TimeOpen = value;
    },
    handleServices(values) {
      this.searchObject.ListServiceNames = values;
    },
    asignCenter() {
      this.searchObject.Coordinates = {
        latitude: +this.center.lat,
        longitude: +this.center.lng,
      };

      fetch(
        `https://maps.googleapis.com/maps/api/geocode/json?latlng=${+this.center
          .lat},${+this.center.lng}&key=${window.__congfigGoogleMapAPI}`
      )
        // https://maps.googleapis.com/maps/api/streetview?size=600x300&location=21.0434405,105.7974636&heading=151.78&pitch=-0.76&key=AIzaSyAQ2Jcz1gngtqWN1w-mtQ2ja1i49EerB50
        .then((response) => response.json())
        .then((data) => {
          console.log(data);

          this.$common.cache.setCache("currentAddress", data);
        })
        .catch((error) => {
          console.error("Lỗi:", error);
        });
    },
    async searchData(newObject) {
      const me = this;
      var result = await GarageAPI.searchGarages(newObject);
      me.locations = result;
    },

    handleShowPostion(index, location) {
      const { googleMapRef } = this;
      if (googleMapRef) {
        let marker = googleMapRef.markers[index];
        googleMapRef.hanleShowInfoWindow(location, marker);
      }
    },
    handleDirecting(index) {
      const { googleMapRef } = this;

      if (googleMapRef) {
        let location = this.locations[index];
        window.open(
          `https://www.google.com/maps/dir/?api=1&destination=${location.latitude},${location.longitude}`,
          "_blank"
        );
      }
    },
  },
};
</script>

<style lang="scss">
@import "./home.scss";
</style>
