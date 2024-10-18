<script setup>
import { reactive } from 'vue';
import ApplicationLogo from '@/components/ApplicationLogo.vue';
import Footer from '@/components/Footer.vue';
import ResponsiveNavLink from '@/components/ResponsiveNavLink.vue';
import ThemeSwitcher from '@/components/ThemeSwitcher.vue';

const dropdown = reactive({
    admin: false,
    menu: false
})
</script>

<template>
    <div class="flex h-screen overflow-hidden bg-gray-100 dark:bg-gray-900">
        <!-- Navbar -->
        <nav
            class="fixed top-0 z-50 w-full bg-white shadow-md dark:bg-gray-800 border-b border-gray-100 dark:border-gray-700">
            <!-- Primary Navigation Menu -->
            <div class="max-w-full mx-auto px-4 sm:px-6 lg:px-8">
                <div class="flex justify-between flex-row-reverse sm:flex-row h-16">
                    <div class="hidden sm:flex">
                        <!-- Logo -->
                        <div class="shrink-0 flex items-center">
                            <router-link to="/" class="text-white mr-4">
                                <ApplicationLogo
                                    class="block h-9 w-auto fill-current text-gray-800 dark:text-gray-200" />
                            </router-link>
                        </div>
                    </div>

                    <div class="flex items-center ms-6">
                        <ThemeSwitcher></ThemeSwitcher>
                    </div>

                    <!-- Hamburger -->
                    <div class="-me-2 flex items-center sm:hidden">
                        <button @click="dropdown.menu = !dropdown.menu"
                            class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 dark:text-gray-500 hover:text-gray-500 dark:hover:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-900 focus:outline-none focus:bg-gray-100 dark:focus:bg-gray-900 focus:text-gray-500 dark:focus:text-gray-400 transition duration-150 ease-in-out">
                            <svg class="h-6 w-6" stroke="currentColor" fill="none" viewBox="0 0 24 24">
                                <path :class="{
                                    hidden: dropdown.menu,
                                    'inline-flex': !dropdown.menu,
                                }" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                    d="M4 6h16M4 12h16M4 18h16" />
                                <path :class="{
                                    hidden: !dropdown.menu,
                                    'inline-flex': dropdown.menu,
                                }" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                                    d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        </button>
                    </div>
                </div>
            </div>

            <!-- Responsive Navigation Menu -->
            <div :class="{ block: showingNavigationDropdown, hidden: !showingNavigationDropdown }" class="sm:hidden">
                <div class="pt-2 pb-3 space-y-1">
                    <ResponsiveNavLink to="/" class="text-white mr-4">
                        Dashboard
                    </ResponsiveNavLink>
                </div>

                <!-- Responsive Settings Options -->
                <div class="pt-4 pb-1 border-t border-gray-200 dark:border-gray-600">
                    <div class="px-4">
                        <div class="font-medium text-base text-gray-800 dark:text-gray-200">
                            <!-- {{ $page.props.auth.user.name }} --> Name
                        </div>
                        <div class="font-medium text-sm text-gray-500">
                            <!-- {{ $page.props.auth.user.email }} --> Email
                        </div>
                    </div>

                    <div class="mt-3 space-y-1">
                        <ResponsiveNavLink> Profile </ResponsiveNavLink>
                        <ResponsiveNavLink method="post" as="button">
                            Log Out
                        </ResponsiveNavLink>
                    </div>
                </div>
            </div>
        </nav>

        <!-- Sidebar -->
        <aside id="sidebar-multi-level-sidebar"
            class="fixed top-0 left-0 z-40 w-64 shadow-md h-screen transition-transform sm:translate-x-0" :class="{ '-translate-x-full': !dropdown.menu }">
            <div class="h-full mt-16 px-3 py-4 overflow-y-auto bg-white dark:bg-gray-800">
                <div class="flex mb-5 sm:hidden">
                    <!-- Logo -->
                    <div class="shrink-0 flex items-center">
                        <router-link to="/">
                            <ApplicationLogo class="block h-9 w-auto fill-current text-gray-800 dark:text-gray-200" />
                        </router-link>
                    </div>
                </div>
                <ul class="space-y-2 font-medium">
                    <li>
                        <router-link to="/"
                            class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">
                            <i class="fa-solid fa-chart-line"></i>
                            <span class="ms-3">Dashboard</span>
                        </router-link>
                    </li>
                    <li>
                        <router-link to="/add-product"
                            class="flex items-center p-2 text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700 group">
                            <i class="fa-solid fa-plus"></i>
                            <span class="ms-3">Add Product</span>
                        </router-link>
                    </li>

                </ul>
            </div>
        </aside>

        <!-- Page Content -->
        <div
            class="pt-16 px-8 relative w-full h-full flex flex-col min-h-screen overflow-y-auto bg-gray-50 sm:ml-64 dark:bg-gray-900">
            <main>
                <router-view />
            </main>

            <!-- Footer -->
            <Footer />
        </div>
    </div>
</template>
