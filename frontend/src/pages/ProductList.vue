<script setup>
import { ref, onMounted, computed } from 'vue';
import Swal from 'sweetalert2';
import axios from 'axios';

const products = ref([]);
const currentPage = ref(1);
const itemsPerPage = ref(5); // Set items per page
const totalProducts = ref(0); // Total number of products

// Fetch all products at once
const fetchAllProducts = async () => {
    try {
        const response = await axios.get('http://localhost:5093/api/product');
        products.value = response.data; // Assume response contains an array of products
        totalProducts.value = response.data.length; // Set total products count
    } catch (error) {
        console.error('Error fetching products:', error);
    }
};

// Slice products based on current page and items per page
const paginatedProducts = computed(() => {
    const start = (currentPage.value - 1) * Number(itemsPerPage.value);
    const end = start + Number(itemsPerPage.value);
    
    return products.value.slice(start, end);
});

// Pagination calculation
const totalPages = computed(() => Math.ceil(totalProducts.value / itemsPerPage.value));

// Handle deleting products
const deleteProduct = async (id) => {
    const result = await Swal.fire({
        title: 'Are you sure?',
        text: 'You won’t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    });

    if (result.isConfirmed) {
        try {
            await axios.delete(`http://localhost:5093/api/product/${id}`);
            await fetchAllProducts(); // Refresh the product list after deletion
            Swal.fire('Deleted!', 'Your product has been deleted.', 'success');
        } catch (error) {
            console.error('Error deleting product:', error);
            Swal.fire('Error!', 'There was an error deleting the product.', 'error');
        }
    }
};

// Fetch all products when the component is mounted
onMounted(fetchAllProducts);

// Pagination controls

const goToPage = (page) => {
    currentPage.value = page;
}

const nextPage = () => {
    if (currentPage.value < totalPages.value) {
        currentPage.value++;
    }
};

const prevPage = () => {
    if (currentPage.value > 1) {
        currentPage.value--;
    }
};

// Currency formatting function
const formatCurrency = (value) => {
    if (typeof value === 'number') {
        return new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'MYR',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        }).format(value);
    }
    return value; // Return the original value if it's not a number
};
</script>

<template>
    <section class="bg-white dark:bg-gray-800 rounded shadow mt-8 p-8">
        <div class="flex flex-wrap justify-between items-center mb-4">
            <h2 class="text-2xl font-bold text-gray-900 dark:text-white">Products</h2>
            <div>
                <label for="itemsPerPage" class="mr-2 text-gray-500 dark:text-gray-300">Items per page:</label>
                <select id="itemsPerPage" v-model="itemsPerPage" class=" border border-gray-300 text-gray-700 dark:bg-gray-700 dark:border-gray-600 dark:text-gray-300 rounded-md p-1">
                    <option value="1">1</option>
                    <option value="5">5</option>
                    <option value="10">10</option>
                    <option value="20">20</option>
                </select>
            </div>
        </div>
        <div class="relative overflow-x-auto">
            <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400 mt-4">
                <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr class="bg-gray-200 dark:bg-gray-700">
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Name</th>
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Brand</th>
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Price</th>
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Category</th>
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Quantity</th>
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Description</th>
                        <th class="py-2 px-4 border-b dark:border-gray-600 dark:text-white">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="product in paginatedProducts" :key="product.id"
                        class="hover:bg-gray-100 dark:hover:bg-gray-600">
                        <td class="py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">{{ product.name }}</td>
                        <td class="py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">{{ product.brand }}</td>
                        <td class="py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">{{ formatCurrency(product.price) }}</td>
                        <td class="py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">{{ product.categoryName }}</td>
                        <td class="py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">{{ product.quantity }}</td>
                        <td class="py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">{{ product.description }}</td>
                        <td class="flex py-2 px-4 border-b dark:border-gray-600 dark:text-gray-300">
                            <button
                                class="cursor-pointer transition-all bg-blue-500 text-white px-4 py-2 rounded-lg border-blue-600 border-b-[4px] hover:brightness-110 hover:-translate-y-[2px] active:border-b-[2px] active:brightness-90 active:translate-y-[2px] mr-2">
                                <router-link :to="`/add-product/${product.id}`"><i class="fas fa-edit"></i></router-link>
                            </button>
                            <button @click="deleteProduct(product.id)"
                                class="cursor-pointer transition-all bg-red-500 text-white px-4 py-2 rounded-lg border-red-600 border-b-[4px] hover:brightness-110 hover:-translate-y-[2px] active:border-b-[2px] active:brightness-90 active:translate-y-[2px]">
                                <i class="fas fa-trash"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!-- Pagination Controls -->
        <div class="flex flex-wrap justify-between items-center p-4 text-gray-500 dark:text-gray-300">
            <span>Showing {{ currentPage }} to {{ itemsPerPage }} of {{ totalPages }}
                entries</span>
            <div class="flex items-center">
                <button @click="prevPage" :disabled="currentPage === 1"
                    class="px-3 py-1.5 border dark:border-gray-700 rounded-l-lg disabled:opacity-30 disabled:cursor-not-allowed">
                    <i class="fas fa-angle-left"></i>
                </button>

                <!-- Render page numbers -->
                <button v-for="page in totalPages" :key="page" @click="goToPage(page)"
                    :class="['px-3 py-1.5 border dark:border-gray-700', { 'bg-gray-100 dark:bg-gray-700 ': currentPage === page }]">
                    {{ page }}
                </button>

                <button @click="nextPage" :disabled="currentPage === totalPages"
                    class="px-3 py-1.5 border dark:border-gray-700 rounded-r-lg disabled:opacity-30 disabled:cursor-not-allowed">
                    <i class="fas fa-angle-right"></i>
                </button>
            </div>
        </div>

    </section>
</template>
