import "../TaskModal/TaskModal.css";
import { UI_TEXT } from "../../constants/uiText";

function ConfirmationModal({
    isOpen,
    title,
    message,
    onConfirm,
    onCancel
}) {

    if (!isOpen) {
        return null;
    }

    return (
        <div className="modal-overlay">

            <div className="modal">

                <h2>{title}</h2>

                <p>{message}</p>

                <div className="modal-actions">

                    <button
                        className="btn-danger"
                        onClick={onConfirm}
                    >
                        {UI_TEXT.BUTTONS.DELETE}
                    </button>

                    <button
                        className="btn-secondary"
                        onClick={onCancel}
                    >
                        {UI_TEXT.BUTTONS.CANCEL}
                    </button>

                </div>

            </div>

        </div>
    );
}

export default ConfirmationModal;