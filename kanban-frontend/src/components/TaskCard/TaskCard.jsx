import { useState } from "react";

import ConfirmationModal from "../ConfirmationModal/ConfirmationModal";
import { useTasks } from "../../context/TaskContext";

function TaskCard({ task }) {

    const { deleteTask } = useTasks();

    const [showDeleteModal, setShowDeleteModal] = useState(false);

    async function handleDelete() {

        try {

            const deleted = await deleteTask(task.id);

            if (deleted) {
                setShowDeleteModal(false);
            }

        } catch (error) {

            console.error(error);

            alert("Unable to delete task.");
        }
    }

    return (
        <div className="task-card">

            <h3>{task.title}</h3>

            <p>{task.description}</p>

            <div className="task-card-dates">

                <div>
                    <strong>Created:</strong> {new Date(task.createdAt).toLocaleString()}
                </div>

                <div>
                    <strong>Updated:</strong> {new Date(task.updatedAt).toLocaleString()}
                </div>

            </div>

            <button onClick={() => setShowDeleteModal(true)}>
                Delete
            </button>

            <ConfirmationModal
                isOpen={showDeleteModal}
                title="Delete Task"
                message="Are you sure you want to delete this task?"
                onConfirm={handleDelete}
                onCancel={() => setShowDeleteModal(false)}
            />

        </div>
    );
}

export default TaskCard;