<script setup lang="ts">
import { ref } from 'vue';
import { API } from '@/api/apiService';
import { Product } from '@/models/product';

const list = ref<Product[]>([]);
const onlyEnabled = ref<boolean>(true);

async function getList() {
   list.value = [];
   const newValues = await API.product.getProducts(onlyEnabled.value);
   list.value = newValues;
}

async function getListByStream() {
   list.value = [];
   await API.product.getProductStream(onlyEnabled.value, (prodArr) => {
      if (prodArr != undefined && prodArr.length > 0) {
         list.value.push(...prodArr);
      }
   });
}
</script>
<template>
   <div class="panel">
      <input id="onlyEnabledChb" v-model="onlyEnabled" type="checkbox" />
      <label for="onlyEnabledChb"> only enabled? </label>
      <button @click="getList">Classic load</button>
      <button @click="getListByStream">Stream load</button>
   </div>
   <div v-for="item in list" :key="item.id" class="product">
      {{ item.name }}
   </div>
</template>

<style lang="scss" scoped>
.product {
   margin: 10px;
}

.panel {
   display: flex;
   flex-direction: row;
   flex-wrap: nowrap;
   justify-content: center;
   align-items: baseline;
}
</style>
