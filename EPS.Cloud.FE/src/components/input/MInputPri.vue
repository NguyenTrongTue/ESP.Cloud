<template>
  <div class="input-default" :class="error ? 'error' : ''">
    <div class="input-label-wrapper" v-if="label">
      <span class="input-label">{{ label }}</span>
      <span class="input-required">{{
        rules?.includes("required") ? " *" : ""
      }}</span>
    </div>

    <div class="input-wrapper">
      <component
        :is="typeComponent"
        class="input"
        ref="minput"
        :type="typeInput ? typeInput : 'text'"
        :rules="rules"
        :name="name"
        :error="error"
        :value="dataValue"
        :style="{ 'text-align': textAlign }"
        :placeholder="placeholderInput"
        :tabindex="tabIndex"
        @input="onInput"
        @focus="$event.target.select()"
        @blur="onBlur(name)"
      />
      <component
        :is="showIcon2 ? icon2 : icon1"
        class="input__icon"
        @click="clickIcon"
      ></component>
    </div>
    <div v-show="error" class="input-error">{{ error }}</div>
  </div>
</template>

<script>
const ALLOWED_KEYS = ["Tab", "Backspace", "Delete"];

import { upperCaseName } from "@/utils/common.js";
import { validate } from "@/utils/validate.js";
export default {
  name: "InputPri",
  props: {
    label: {
      type: String,
      default: "",
    },
    typeInput: {
      type: String,
    },
    allowNumber: {
      type: Boolean,
      default: false,
    },
    allowDecimal: {
      type: Boolean,
      default: false,
    },
    modelValue: {
      type: String,
    },
    rules: {
      type: String,
    },
    textAlign: {
      type: String,
    },
    placeholderInput: {
      type: String,
      default: "",
    },
    tabIndex: {
      type: String,
    },
    formName: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "",
    },
    typeComponent: {
      type: String,
      default: "input",
    },
    icon1: {
      type: Object,
      required: true,
    },
    icon2: {
      type: Object,
      required: true,
    },
    clickIcon: {
      type: Function,
      required: false,
    },
  },

  data() {
    return {
      dataValue: "",
      error: "",
      showIcon2: true,
    };
  },
  created() {
    this.assignDataValue(this.modelValue);
  },
  watch: {
    modelValue(newValue) {
      this.assignDataValue(newValue);
    },
  },

  methods: {
    /**
     * Format lại giá trị hiển thị trong input theo định dạng.
     * @param {*} value Giá trị của input
     */
    assignDataValue(value) {
      if (this.allowNumber || this.allowDecimal) {
        if (value) {
          let formattedString = Number(value).toLocaleString("vi-VN");
          this.dataValue = formattedString;
        } else {
          return "";
        }
      } else {
        this.dataValue = value;
      }
    },

    /**
     * @description Handles the input event when the user types into the input field
     * @author nttue
     * Modified at (10/07/2023)
     */
    onInput(event) {
      if (this.allowNumber) {
        const value = +event.target.value.split(".").join("");
        this.$emit("update:modelValue", value);
      } else if (this.allowDecimal) {
        const value = event.target.value.split(".").join("");
        const newValue = value.split(",");
        let number = 0;
        if (!newValue[1]) {
          number = +newValue[0];
        } else {
          number = parseFloat(newValue[0] + "." + newValue[1]);
        }
        this.$emit("update:modelValue", number);
      } else {
        this.dataValue = event.target.value;
        this.$emit("update:modelValue", event.target.value);
      }

      if (this.rules) {
        this.validateData();
      }
    },
    /**
     * Function to validate the data based on the specified rules.
     * @author nttue 03.03.2024
     */
    validateData() {
      for (const rule of this.rules.split("|")) {
        const err = validate[rule](
          this.dataValue,
          this.$MResources[this.formName][this.name][rule]
        );
        if (err) {
          this.error = err;
          return;
        }
      }
      this.error = "";
    },
    /**
     * Select and focus the input when it is focused.
     * @author nttue 03.03.2024
     */
    focus() {
      this.$nextTick(() => {
        this.$refs.input.select();
        this.$refs.input.focus();
      });
    },

    /**
     * @description hàm xử lý sự kiện khi người dùng blur khỏi input
     * @param {name} name tên của input
     */
    onBlur(name) {
      if (this.type && this.dataValue) {
        this.dataValue = upperCaseName(this.dataValue);
      }
      if (
        this.rules?.includes("required") ||
        (this.rules && this.dataValue.trim())
      ) {
        this.validateData();
      }
    },
  },
};
</script>

<style lang="scss">
@import "./input.scss";
</style>
