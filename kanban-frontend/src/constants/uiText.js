import { VALIDATION } from "./kanbanConstants";

export const UI_TEXT = {
    KANBAN_BOARD_PAGE_TITLE: "Kanban Task Board",

    BUTTONS: {
        ADD_TASK: "Add Task",
        CREATE: "Create",
        UPDATE: "Update",
        CANCEL: "Cancel",
        DELETE: "Delete",
        EDIT: "Edit"
    },

    FORM: {
        TITLE_PLACEHOLDER: "Title",
        DESCRIPTION_PLACEHOLDER: "Description"
    },

    TASK_CARD: {
        CREATED: "Created:",
        UPDATED: "Updated:"
    },

    MODALS: {
        ADD_TITLE: "Add Task",
        EDIT_TITLE: "Edit Task",
        DELETE_TITLE: "Delete Task",
        DELETE_MESSAGE: "Are you sure you want to delete this task?"
    },

    VALIDATION: {
        TITLE_REQUIRED: "Title is required",
        TITLE_MAX: `Title must be ${VALIDATION.TITLE_MAX_LENGTH} characters or less`,
        DESCRIPTION_MAX: `Description must be ${VALIDATION.DESCRIPTION_MAX_LENGTH} characters or less`
    },

    MESSAGES: {
        LOADING_TASKS: "Loading tasks...",
        CREATE_ERROR: "Unable to create task.",
        UPDATE_ERROR: "Unable to update task.",
        SAVE_ERROR: "Unable to save task.",
        DELETE_ERROR: "Unable to delete task."
    },

    TOOLTIPS: {
        DRAG_TASK: "Drag to move task"
    }
};