<template>
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="24"
    height="24"
    fill="currentColor"
    viewBox="0 0 256 256"
    class="ds-password-field-show-password"
    aria-hidden="true"
  >
    <rect width="256" height="256" fill="none"></rect>
    <path
      d="M128,56C48,56,16,128,16,128s32,72,112,72,112-72,112-72S208,56,128,56Z"
      fill="none"
      stroke="currentColor"
      stroke-linecap="round"
      stroke-linejoin="round"
      stroke-width="16"
    ></path>
    <circle
      cx="128"
      cy="128"
      r="40"
      fill="none"
      stroke="currentColor"
      stroke-linecap="round"
      stroke-linejoin="round"
      stroke-width="16"
    ></circle>
  </svg>
</template>

<script>
export default {
  props: {
    typeArrow: {
      type: String,
      default: "left",
    },
  },
  name: "Eye",
};
</script>
