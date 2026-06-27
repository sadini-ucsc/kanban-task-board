import { createContext, useContext, useEffect, useReducer } from "react";
import { getTasks } from "../api/taskApi";
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

        }
        catch (error) {

            dispatch({
                type: "LOAD_TASKS_FAILURE",
                payload: error.message
            });

        }

    }

    return (

        <TaskContext.Provider
            value={{
                tasks: state.tasks,
                loading: state.loading,
                error: state.error,
                loadTasks
            }}
        >

            {children}

        </TaskContext.Provider>

    );

}

export function useTasks() {

    return useContext(TaskContext);

}