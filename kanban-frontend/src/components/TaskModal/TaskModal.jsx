import { useState, useEffect } from "react";

import { useTasks } from "../../context/TaskContext";
import "./TaskModal.css";
import { UI_TEXT } from "../../constants/uiText";

function TaskModal({ isOpen, onClose, task }) {

    const { createTask, updateTask } = useTasks();

    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");

    const [errors, setErrors] = useState({});
    const [apiError, setApiError] = useState("");

    useEffect(() => {

        if (!isOpen) {
            return;
        }

        if (task) {

            setTitle(task.title);
            setDescription(task.description);

        } else {

            setTitle("");
            setDescription("");
        }

        setErrors({});
        setApiError("");

    }, [isOpen, task]);

    if (!isOpen) return null;

    function validate() {

        const newErrors = {};

        if (!title.trim()) {
            newErrors.title = UI_TEXT.VALIDATION.TITLE_REQUIRED;
        } else if (title.length > 100) {
            newErrors.title = UI_TEXT.VALIDATION.TITLE_MAX;
        }

        if (description.length > 1000) {
            newErrors.description = UI_TEXT.VALIDATION.DESCRIPTION_MAX;
        }

        setErrors(newErrors);

        return Object.keys(newErrors).length === 0;
    }

    async function handleSubmit(e) {

        e.preventDefault();

        setApiError("");

        if (!validate()) {
            return;
        }

        try {

            if (task) {

                await updateTask({
                    id: task.id,
                    title,
                    description,
                    status: task.status,
                    createdAt: task.createdAt
                });

            } else {

                await createTask({
                    title,
                    description
                });
            }

            setTitle("");
            setDescription("");
            setErrors({});
            setApiError("");

            onClose();

        } catch (error) {

            const message =
                error.response?.data?.error ??
                error.response?.data?.message ??
                UI_TEXT.MESSAGES.SAVE_ERROR;

            setApiError(message);
        }
    }

    return (
        <div className="modal-overlay">

            <div className="modal">

                <h2>
                    {task
                        ? UI_TEXT.MODALS.EDIT_TASK_TITLE
                        : UI_TEXT.MODALS.ADD_TASK_TITLE}
                </h2>

                <form onSubmit={handleSubmit}>

                    <input
                        placeholder={UI_TEXT.FORM.TITLE_PLACEHOLDER}
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />

                    {errors.title && (
                        <div className="error-text">{errors.title}</div>
                    )}

                    <br/>

                    <textarea
                        placeholder={UI_TEXT.FORM.DESCRIPTION_PLACEHOLDER}
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />

                    {errors.description && (
                        <div className="error-text">{errors.description}</div>
                    )}

                    {apiError && (
                        <div className="error-text">
                            {apiError}
                        </div>
                    )}

                    <div className="modal-actions">

                        <button type="submit" className="btn-primary">
                            {task ? UI_TEXT.BUTTONS.UPDATE : UI_TEXT.BUTTONS.CREATE}
                        </button>

                        <button type="button" className="btn-secondary">
                            {UI_TEXT.BUTTONS.CANCEL}
                        </button>

                    </div>

                </form>

            </div>

        </div>
    );
}

export default TaskModal;