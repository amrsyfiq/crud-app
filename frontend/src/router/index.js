import { createRouter, createWebHistory } from 'vue-router'; // Update imports
import ProductList from '../pages/ProductList.vue';
import AddProduct from '../pages/AddProduct.vue';

const routes = [
  {
    path: '/',
    name: 'ProductList',
    component: ProductList,
  },
  {
    path: '/add-product/:id?',
    name: 'AddProduct',
    component: AddProduct,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
