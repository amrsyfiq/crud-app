<script setup>
import { ref, reactive, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import axios from 'axios';
import Swal from 'sweetalert2';


const route = useRoute();
const router = useRouter();

let form = reactive({
    id: null,
    name: null,
    price: null,
    brand: null,
    categoryId: null,
    quantity: null,
    description: null,
});

const categories = ref([]); // Reactive variable to hold categories

const addProduct = async () => {
    try {
        if (form.id) {
            // Update product
            await axios.put(`http://localhost:5093/api/product/${form.id}`, form);
            await Swal.fire({
                icon: 'success',
                title: 'Product Updated',
                text: 'The product has been updated successfully!',
                confirmButtonColor: '#3085d6',
            });
        } else {
            // Create new product
            const productData = {
                name: form.name,
                price: form.price,
                brand: form.brand,
                categoryId: form.categoryId,
                quantity: form.quantity,
                description: form.description,
            };
            await axios.post('http://localhost:5093/api/product', productData);
            await Swal.fire({
                icon: 'success',
                title: 'Product Added',
                text: 'The product has been added successfully!',
                confirmButtonColor: '#3085d6',
            });
        }
        router.push('/'); // Redirect to the product list after adding/updating
    } catch (error) {
        // Display an error notification with SweetAlert
        await Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error adding/updating product: ' + (error.response?.data || 'An unexpected error occurred.'),
            confirmButtonColor: '#3085d6',
        });
        console.error('Error adding/updating product:', error.response ? error.response.data : error);
    }

};

// Fetch product data
const fetchProduct = async () => {
    const id = route.params.id;
    if (id) {
        try {
            const response = await axios.get(`http://localhost:5093/api/product/${id}`);
            Object.assign(form, response.data); // Use Object.assign to maintain reactivity
        } catch (error) {
            console.error('Error fetching product:', error);
        }
    }
};

// Fetch categories from the backend
const fetchCategories = async () => {
    try {
        const response = await axios.get('http://localhost:5093/api/category');
         // Ensure this API exists
        categories.value = response.data; // Assuming response.data is an array of categories
    } catch (error) {
        console.error('Error fetching categories:', error);
    }
};

// Fetch product and categories data when the component is mounted
onMounted(() => {
    fetchProduct();
    fetchCategories();
});
</script>

<template>
    <section class="bg-white dark:bg-gray-800 rounded shadow mt-8">
        <router-link to="/" class="absolute dark:text-white"><i class="fas fa-arrow-left transition-all hover:-translate-x-[2px] rounded bg-gray-100 dark:bg-gray-700 px-2 py-1.5 m-6"></i></router-link>
        <div class="py-8 px-4 mx-auto max-w-2xl lg:py-16">
            <h2 v-if="form.id" class="mb-8 text-2xl font-bold text-gray-900 dark:text-white">Edit product</h2>
            <h2 v-else class="mb-8 text-2xl font-bold text-gray-900 dark:text-white">Add a new product</h2>
            <form @submit.prevent="addProduct">
                <div class="grid gap-4 sm:grid-cols-2 sm:gap-6">
                    <div class="sm:col-span-2">
                        <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Product Name</label>
                        <input v-model="form.name" type="text" name="name" id="name" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-600 focus:border-blue-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Type product name" required="">
                    </div>
                    <div class="w-full">
                        <label for="brand" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Brand</label>
                        <input v-model="form.brand" type="text" name="brand" id="brand" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-600 focus:border-blue-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Product brand" required="">
                    </div>
                    <div class="w-full">
                        <label for="price" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Price</label>
                        <input v-model="form.price" type="number" name="price" id="price" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-600 focus:border-blue-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="$2999" required="">
                    </div>
                    <div>
                        <label for="category" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Category</label>
                        <select v-model="form.categoryId" id="category" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
                            <option disabled value="">Select category</option>
                            <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.name }}</option>
                        </select>
                    </div>
                    <div>
                        <label for="quantity" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Quantity</label>
                        <input v-model="form.quantity" type="number" name="Quantity" id="Quantity" class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-600 focus:border-blue-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="12" required="">
                    </div> 
                    <div class="sm:col-span-2">
                        <label for="description" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Description</label>
                        <textarea v-model="form.description" id="description" rows="8" class="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your description here"></textarea>
                    </div>
                </div>
                <button type="submit" class="cursor-pointer transition-all bg-blue-500 text-white px-6 py-2 rounded-lg
                                border-blue-600
                                border-b-[4px] hover:brightness-110 hover:-translate-y-[2px]
                                active:border-b-[2px] active:brightness-90 active:translate-y-[2px] mt-4">
                    Submit
                </button>
            </form>
        </div>
    </section>
</template>


