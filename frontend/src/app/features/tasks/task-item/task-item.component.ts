import {Component, EventEmitter, Input, Output} from '@angular/core';
import {Task} from "../../../core/models/task";
import {TodoTasksService} from "../../../core/services/todo/todo-tasks.service";

@Component({
    selector: 'app-task-item',
    templateUrl: './task-item.component.html',
    styleUrls: ['./task-item.component.scss']
})
export class TaskItemComponent {
    @Input() task: Task;

    @Output() delete = new EventEmitter<string>();

    constructor(private todoTasksService: TodoTasksService) {
    }

    completeTask() {
        console.log(this.task);

        this.task.isCompleted = !this.task.isCompleted;

        this.todoTasksService.updateTask(this.task.id, this.task.title, this.task.isCompleted)
            .subscribe({
                next: (task) => {
                    this.task.completedAt = task.completedAt;
                },
                error: (err) => {
                    this.task.isCompleted = !this.task.isCompleted;
                }
            });

    }

    deleteTask() {
        this.todoTasksService.deleteTask(this.task.id).subscribe(() => {
            this.delete.emit(this.task.id);
        });
    }
}
