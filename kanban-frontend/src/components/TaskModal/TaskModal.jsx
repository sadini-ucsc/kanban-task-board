import { useState, useEffect } from "react";
import { useTasks } from "../../context/TaskContext";

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

        setApiError("");

    }, [isOpen, task]);

    if (!isOpen) return null;

    function validate() {

        const newErrors = {};

        if (!title.trim()) {
            newErrors.title = "Title is required";
        } else if (title.length > 100) {
            newErrors.title = "Title must be 100 characters or less";
        }

        if (description.length > 1000) {
            newErrors.description = "Description must be 1000 characters or less";
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
                "Unable to save task.";

            setApiError(message);
        }
    }

    return (
        <div className="modal-overlay">

            <div className="modal">

                <h2> {task ? "Edit Task" : "Add Task"} </h2>

                <form onSubmit={handleSubmit}>

                    <input
                        placeholder="Title"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />

                    <br/>

                    {errors.title && (
                        <div className="error-text">{errors.title}</div>
                    )}

                    <br/>

                    <textarea
                        placeholder="Description"
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

                        <button
                            type="submit"
                            className="btn-primary"
                        >
                            {task ? "Update" : "Create"}
                        </button>

                        <button
                            type="button"
                            className="btn-secondary"
                            onClick={onClose}
                        >
                            Cancel
                        </button>

                    </div>

                </form>

            </div>

        </div>
    );
}

export default TaskModal;