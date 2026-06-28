import { TASK_ACTION_TYPES } from "../constants/kanbanConstants";

export const initialState = {
    tasks: [],
    loading: false,
    error: null
};

export function taskReducer(state, action) {

    switch (action.type) {

        case TASK_ACTION_TYPES.SET_LOADING:
            return {
                ...state,
                loading: true,
                error: null
            };

        case TASK_ACTION_TYPES.LOAD_TASKS_SUCCESS:
            return {
                ...state,
                loading: false,
                tasks: action.payload
            };

        case TASK_ACTION_TYPES.LOAD_TASKS_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.payload
            };

        case TASK_ACTION_TYPES.CREATE_TASK_SUCCESS:
            return {
                ...state,
                tasks: [...state.tasks, action.payload]
            };

        case TASK_ACTION_TYPES.DELETE_TASK_SUCCESS:
            return {
                ...state,
                tasks: state.tasks.filter(task => task.id !== action.payload)
            };
            
        case TASK_ACTION_TYPES.UPDATE_TASK_SUCCESS:
            return {
                ...state,
                tasks: state.tasks.map(task =>
                    task.id === action.payload.id
                        ? action.payload
                        : task
                )
            };

        default:
            return state;
    }
}