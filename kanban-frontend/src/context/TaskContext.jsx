import { createContext, useContext, useEffect, useReducer } from "react";
import { getTasks, createTask as createTaskApi } from "../api/taskApi";
import { initialState, taskReducer } from "./TaskReducer";

const TaskContext = createContext();

export function TaskProvider({ children }) {

    const [state, dispatch] = useReducer(taskReducer, initialState);

    useEffect(() => {

        loadTasks();

    }, []);

    async function loadTasks() {

        dispatch({ type: "SET_LOADING" });

        try {

            const tasks = await getTasks();

            dispatch({
                type: "LOAD_TASKS_SUCCESS",
                payload: tasks
            });

        } catch (error) {

            dispatch({
                type: "LOAD_TASKS_FAILURE",
                payload: error.message
            });

        }
    }

    async function createTask(task) {

        try {

            const createdTask = await createTaskApi(task);

            dispatch({
                type: "CREATE_TASK_SUCCESS",
                payload: createdTask
            });

            return createdTask;

        } catch (error) {

            throw error;
        }
    }

    return (

        <TaskContext.Provider
            value={{
                tasks: state.tasks,
                loading: state.loading,
                error: state.error,

                loadTasks,
                createTask
            }}
        >
            {children}
        </TaskContext.Provider>

    );
}

export function useTasks() {
    return useContext(TaskContext);
}