<template>
  <div class="home">
    <SideBar :locationProps="locations" :none-filter="true" @sortBy="handleSortBy" @typeCars="handleTypeCars"
      @openTime="handleOpenTime" @services="handleServices" @showPostion="handleShowPostion" />
    <GoogleMap :center="center" :locations="locations" ref="google-map" />
    <!-- <Test /> -->
  </div>
</template>

<script>
import GoogleMap from "@/components/map/GoogleMap.vue";
import SideBar from "@/components/sidebar/SideBar.vue";
import GarageAPI from "@/apis/GarageAPI";
import BookingAPI from "@/apis/BookingAPI";
export default {
  components: {
    GoogleMap,
    SideBar,
  },
  data() {
    return {
      center: {},
      locations: [],
      searchObject: {
        latitude: 0,
        longitude: 0,
        p_estimate_id: this.$route.params.id,
      },
    };
  },

  async mounted() {
    try {
      let currentCoords =
        this.$ms.cache.getCache("currentAddress")?.results[0]?.geometry
          ?.location;
      if (currentCoords) {
        this.center = {
          lat: currentCoords.lat,
          lng: currentCoords.lng,
        };

        this.searchObject.latitude = +currentCoords.lat;
        this.searchObject.longitude = +currentCoords.lng;

      } else {
        let value = this.$ms.cache.getCache("coords");
        if (value) {
          this.center = {
            lat: value.latitude,
            lng: value.longitude,
          };
          this.searchObject.latitude = +value.latitude;
          this.searchObject.longitude = +value.longitude;

        }

      }
      this.searchData();

    } catch (e) { }
  },
  watch: {
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
    asignCenter() {
      this.searchObject = {
        latitude: +this.center.lat,
        longitude: +this.center.lng,
      };
    },
    async searchData() {
      const me = this;
      var result = await BookingAPI.getGarageByEstimate(me.searchObject);
      me.locations = result;
    },

    handleShowPostion(index, location) {
      const { googleMapRef } = this;
      if (googleMapRef) {
        let marker = googleMapRef.markers[index];
        googleMapRef.hanleShowInfoWindow(location, marker);
      }
    },
  },
};
</script>

<style lang="scss">
@import "./estimate.scss";
</style>
