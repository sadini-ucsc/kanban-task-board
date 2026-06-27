import Column from "../Column/Column";
import { KanbanTaskStatus } from "../../enums/KanbanTaskStatus";

function Board() {

    return (
        <div className="board">

            <Column
                label={KanbanTaskStatus.Todo.label}
                status={KanbanTaskStatus.Todo.value}
            />

            <Column
                label={KanbanTaskStatus.InProgress.label}
                status={KanbanTaskStatus.InProgress.value}
            />

            <Column
                label={KanbanTaskStatus.Done.label}
                status={KanbanTaskStatus.Done.value}
            />


        </div>
    );
}

export default Board;