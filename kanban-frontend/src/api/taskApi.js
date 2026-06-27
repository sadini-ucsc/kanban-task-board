import axios from "axios";

const TASKS_API_URL = `${import.meta.env.VITE_BE_API_BASE_URL}/tasks`;

export async function getTasks() {
    const response = await axios.get(TASKS_API_URL);
    return response.data;
}