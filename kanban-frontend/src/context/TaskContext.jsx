import { createContext, useContext, useEffect, useReducer } from "react";

import { 
    getTasks, 
    createTask as createTaskApi,
    deleteTask as deleteTaskApi,
    updateTask as updateTaskApi } from "../api/taskApi";
import { initialState, taskReducer } from "./TaskReducer";
import { TASK_ACTION_TYPES } from "../constants/kanbanConstants";

const TaskContext = createContext();

export function TaskProvider({ children }) {

    const [state, dispatch] = useReducer(taskReducer, initialState);

    useEffect(() => {

        loadTasks();

    }, []);

    async function loadTasks() {

        dispatch({ type: TASK_ACTION_TYPES.SET_LOADING });

        try {

            const tasks = await getTasks();

            dispatch({
                type: TASK_ACTION_TYPES.LOAD_TASKS_SUCCESS,
                payload: tasks
            });

        } catch (error) {

            dispatch({
                type: TASK_ACTION_TYPES.LOAD_TASKS_FAILURE,
                payload: error.message
            });

        }
    }

    async function createTask(task) {

        try {

            const createdTask = await createTaskApi(task);

            dispatch({
                type: TASK_ACTION_TYPES.CREATE_TASK_SUCCESS,
                payload: createdTask
            });

            return createdTask;

        } catch (error) {

            throw error;
        }
    }

    async function deleteTask(id) {

        try {

            const deleted = await deleteTaskApi(id);

            if (deleted) {

                dispatch({
                    type: TASK_ACTION_TYPES.DELETE_TASK_SUCCESS,
                    payload: id
                });
            }

            return deleted;

        } catch (error) {

            throw error;
        }
    }

    async function updateTask(task) {

        try {

            const updatedTask = await updateTaskApi(task);

            dispatch({
                type: TASK_ACTION_TYPES.UPDATE_TASK_SUCCESS,
                payload: updatedTask
            });

            return updatedTask;

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
                createTask,
                deleteTask,
                updateTask
            }}
        >
            {children}
        </TaskContext.Provider>

    );
}

export function useTasks() {
    return useContext(TaskContext);
}