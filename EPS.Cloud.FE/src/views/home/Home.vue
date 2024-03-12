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
import { useGeolocation } from "@vueuse/core";
import GarageAPI from "@/apis/GarageAPI";
import { setCache, getCache } from "@/utils/cache";

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
        take: 20,
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
    let value = getCache("coords");
    if (value) {
      this.center = {
        lat: value.lat,
        lng: value.lng,
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
          setCache("coords", {
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
        setCache("coords", {
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
        googleMapRef.calculateAndDisplayRoute(location);
      }
    },
  },
};
</script>

<style lang="scss">
@import "./home.scss";
</style>
