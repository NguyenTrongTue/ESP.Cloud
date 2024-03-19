<template>
  <div class="sidebar">
    <div class="sidebar__top">
      <div class="sidebar__top-title">Gara gần tôi</div>
      <div class="filter__wrapper" v-if="!noneFilter">
        <div
          class="filter__item"
          v-for="(item, index) in listFilter"
          :key="index"
          @click="showFilter($event, index)"
        >
          <span>
            {{ item.text }}
          </span>
          <MFilter
            :filterId="item.id"
            :data="item.data"
            :chooseType="item.choose"
            :index="index"
            :modelValue="item.modelValue"
            v-if="currentIndexFilter == index"
            @saveOptions="handleSaveOptions"
            @clearChecked="handleClearChecked"
          />
        </div>
      </div>
    </div>
    <div class="sidebar-item__wrapper">
      <SideBarItem
        v-for="(location, index) in locations"
        :key="location?.garage_id"
        :location="location"
        :garaIndex="index"
        image="https://storage.googleapis.com/rp-production-public-content/3z624a2usqnwc07nb8ni93deixc7"
        @mouseenter="handleMouseMove(index, location)"
      />
    </div>
  </div>
</template>

<script>
import SideBarItem from "@/components/sidebar-item/SideBarItem.vue";
import MFilter from "@/components/filter/MFilter.vue";
import GarageAPI from "@/apis/GarageAPI";
export default {
  name: "Sidebar",
  components: {
    SideBarItem,
    MFilter,
  },
  props: {
    locationProps: {
      type: Array,
      default: [],
    },
    noneFilter: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      listFilter: [
        {
          text: "Loại xe",
          id: "typeCars",
          data: [],
          choose: "radio",
          modelValue: "",
        },
        {
          text: "Sắp xếp theo",
          id: "sortBy",
          modelValue: "",
          data: [
            {
              text: "Khoảng cách",
              value: "distance",
              id: 1,
            },
            {
              text: "Đánh giá cao nhất",
              value: "total_rating",
              id: 2,
            },
          ],
          choose: "radio",
        },
        {
          id: "openTime",
          text: "Giờ mở cửa",
          modelValue: "",
          data: [
            {
              text: "Ngay bây giờ",
              value: 0,
              id: 1,
            },
            {
              text: "Sau giờ làm việc",
              value: 1,
              id: 2,
            },
            {
              text: "Trước giờ làm việc",
              value: 2,
              id: 3,
            },
            {
              text: "Tất cả thời gian",
              value: 3,
              id: 4,
            },
          ],
          choose: "radio",
        },
        {
          id: "services",
          text: "Dịch vụ",
          data: [],
          choose: "checkbox",
        },
      ],
      locations: [],
      currentIndexFilter: -1,
    };
  },
  emits: ["sortBy", "typeCars", "openTime", "showPosition"],
  mounted() {
    this.initialData();
  },
  watch: {
    locationProps(newLocations) {
      this.locations = newLocations;
    },
  },
  methods: {
    handleClearChecked(index) {
      this.listFilter[3].data = this.listFilter[3].data.map((item) => {
        return {
          ...item,
          value: false,
        };
      });
      this.$emit("services", []);
      this.toggleFilter(index);
    },
    handleSaveOptions(filterId, value, index) {
      switch (filterId) {
        case "typeCars":
          this.listFilter[0].modelValue = value;
          this.$emit("typeCars", value);
          break;
        case "sortBy":
          this.listFilter[1].modelValue = value;
          if (value == "distance") {
            this.$emit("sortBy", "distance asc");
          } else if (value == "total_rating") {
            this.$emit("sortBy", "total_rating desc");
          }
          break;
        case "openTime":
          this.listFilter[2].modelValue = value;
          this.$emit("openTime", value ? value : 0);
          break;
        case "services":
          this.$emit(
            "services",
            this.listFilter[3].data
              .filter((item) => item.value)
              .map((item) => item.text)
          );
          break;
      }

      this.toggleFilter(index);
    },
    toggleFilter(index) {
      if (this.currentIndexFilter != index) {
        this.currentIndexFilter = index;
      } else {
        this.currentIndexFilter = -1;
      }
    },
    async initialData() {
      let carsList = this.$common.cache.getCache("cars");
      let servicesList = this.$common.cache.getCache("services");
      if (carsList && carsList.length >= 0) {
        this.listFilter[0].data = [...carsList];
      } else {
        let cars = await GarageAPI.getCars();
        let dataConvert = cars.map((item, index) => {
          return {
            text: item.make,
            value: item.make,
            id: index,
          };
        });
        this.listFilter[0].data = [...dataConvert];
        this.$common.cache.setCache("cars", dataConvert);
      }

      if (servicesList && servicesList.length >= 0) {
        this.listFilter[3].data = [...servicesList];
      } else {
        let services = await GarageAPI.getGarageServices();
        let dataConvert = services.map((item, index) => {
          return {
            text: item.service_name,
            value: false,
            id: index,
          };
        });
        this.listFilter[3].data = [...dataConvert];
        this.$common.cache.setCache("services", dataConvert);
      }
    },
    showFilter(event, index) {
      if (this.findParent(event.target, "filter-gara")) {
        return;
      } else {
        this.toggleFilter(index);
      }
    },

    findParent(currentTag, targetTag) {
      var tag = currentTag,
        result = false;
      while (!result && tag.className != "filter__item") {
        if (tag.className.includes(targetTag)) {
          result = true;
        } else {
          tag = currentTag.parentElement;
        }
      }

      return result;
    },

    handleMouseMove(index, location) {
      this.$emit("showPostion", index, location);
    },
  },
};
</script>

<style lang="scss">
@import "./sidebar.scss";
</style>
