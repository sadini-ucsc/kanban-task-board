export const initialState = {
    tasks: [],
    loading: false,
    error: null
};

export function taskReducer(state, action) {

    switch (action.type) {

        case "SET_LOADING":
            return {
                ...state,
                loading: true,
                error: null
            };

        case "LOAD_TASKS_SUCCESS":
            return {
                ...state,
                loading: false,
                tasks: action.payload
            };

        case "LOAD_TASKS_FAILURE":
            return {
                ...state,
                loading: false,
                error: action.payload
            };

        default:
            return state;
    }
}