<template>
  <button v-if="type == 'icon'" class="btn icon">
    <component :is="icon" class="btn-icon"></component>
    {{ buttonText }}
  </button>
  <button v-else class="btn">{{ buttonText }}</button>
</template>

<script>
export default {
  name: "MButton",
  props: {
    buttonText: {
      type: String,
      default: "",
    },

    type: {
      validator(value) {
        return ["default", "icon"].includes(value);
      },
      type: String,
      default: "default",
    },

    icon: {
      type: Object,
      required: true,
    },
  },
  data() {},
  watch: {},

  methods: {},
};
</script>

<style lang="scss" scoped>
@import "./button.scss";
</style>
