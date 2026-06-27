import axios from "axios";

const TASKS_API_URL = `${import.meta.env.VITE_BE_API_BASE_URL}/tasks`;

export async function getTasks() {

    const response = await axios.get(TASKS_API_URL);

    return response.data;

}

export async function createTask(task) {

    const response = await axios.post(TASKS_API_URL, task);

    return response.data;

}

export async function deleteTask(id) {

    const response = await axios.delete(`${TASKS_API_URL}/${id}`);

    return response.data;
}