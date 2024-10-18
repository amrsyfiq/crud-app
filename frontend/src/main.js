import { createApp } from 'vue'; // Update import
import App from './App.vue';
import router from './router'; // Import the router
import '@fortawesome/fontawesome-free/css/all.css';
import './assets/tailwind.css';
import 'flowbite/dist/flowbite.css';
import 'flowbite';
import 'simple-datatables';

const app = createApp(App); // Create the app instance
app.use(router); // Use the router
app.mount('#app'); // Mount the app
