<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

const product = ref({
    id: null,
    name: '',
    price: null,
    quantity: null,
});

// Fetch product data
const fetchProduct = async () => {
    const id = route.params.id;
    try {
        const response = await axios.get(`http://localhost:5093/api/product/${id}`);
        product.value = response.data;
    } catch (error) {
        console.error('Error fetching product:', error);
    }
};

// Update product
const updateProduct = async () => {
    try {
        await axios.put(`http://localhost:5093/api/product/${product.value.id}`, product.value);
        router.push('/'); // Redirect to the product list after updating
    } catch (error) {
        console.error('Error updating product:', error);
    }
};

// Fetch product data when the component is mounted
onMounted(fetchProduct);
</script>
<template>
    <div class="container mx-auto mt-8">
        <h2 class="text-2xl font-bold mb-4 text-gray-900 dark:text-white">Edit Product</h2>
        <form @submit.prevent="updateProduct" class="bg-white p-6 rounded shadow-md dark:bg-gray-800">
            <div class="mb-4">
                <label class="block text-gray-700 dark:text-gray-300">Product Name</label>
                <input v-model="product.name" type="text" class="border rounded w-full py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-gray-600" required />
            </div>
            <div class="mb-4">
                <label class="block text-gray-700 dark:text-gray-300">Price</label>
                <input v-model="product.price" type="number" class="border rounded w-full py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-gray-600" required />
            </div>
            <div class="mb-4">
                <label class="block text-gray-700 dark:text-gray-300">Quantity</label>
                <input v-model="product.quantity" type="number" class="border rounded w-full py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-gray-600" required />
            </div>
            <button type="submit" class="bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-600 dark:bg-blue-600 dark:hover:bg-blue-700">Update Product</button>
            <router-link to="/" class="ml-2 bg-gray-300 text-black py-2 px-4 rounded hover:bg-gray-400 dark:bg-gray-600 dark:text-white dark:hover:bg-gray-500">Cancel</router-link>
        </form>
    </div>
</template>