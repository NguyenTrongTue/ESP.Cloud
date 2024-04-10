<script>
import BookingAPI from "@/apis/BookingAPI";

export default {
    name: "carMixin",

    data() {
        return {
            makeList: [],
            yearList: [],
            modelList: [],
            garageId: "00000000-0000-0000-0000-000000000000",
            objectMaster: {
                make: null,
                year: null,
                model: null
            }
        }
    },

    watch: {
        "objectMaster.make"(newValue) {
            if (newValue) {
                this.fetchYears();
                this.objectMaster.year = null;
                this.objectMaster.model = null;
            }
        },
        "objectMaster.year"(newValue) {
            if (newValue) {
                this.fetchModel();
                this.objectMaster.model = null;
            }
        },
        "objectMaster.model"(newValue) {
            if (newValue) {
                this.objectMaster.cars_id = this.modelAndIdCarList.find(
                    (item) => item.model == newValue
                ).cars_id;
            }
        },
    },
    methods: {
        async fetchMakes() {
            try {
                const result = await BookingAPI.getMakeByGarageId(this.garageId);
                this.makeList = result.map((item) => item.make);
            } catch {
                // ignore error
            }
        },
        async fetchYears() {
            try {
                const result = await BookingAPI.getYearsByGarageIdAndMake(
                    this.garageId,
                    this.objectMaster.make
                );
                this.yearList = result.map((item) => item.year);
            } catch {
            }
        },
        async fetchModel() {
            try {
                const result = await BookingAPI.getModelsByGarageId(
                    this.garageId,
                    this.objectMaster.make,
                    this.objectMaster.year
                );
                this.modelList = result.map((item) => item.model);
                this.modelAndIdCarList = result;
            } catch {
            }
        },
    },
}

</script>