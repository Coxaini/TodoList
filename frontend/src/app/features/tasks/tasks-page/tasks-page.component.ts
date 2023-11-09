import {Component} from '@angular/core';
import {TodoTasksService} from "../../../core/services/todo/todo-tasks.service";
import {Task} from "../../../core/models/task";
import {FormControl, Validators} from "@angular/forms";

@Component({
    selector: 'app-tasks-page',
    templateUrl: './tasks-page.component.html',
    styleUrls: ['./tasks-page.component.scss']
})
export class TasksPageComponent {

    tasks: Task[] = [];

    taskInput = new FormControl('', [Validators.required, Validators.maxLength(300)]);

    constructor(private todoService: TodoTasksService) {
        todoService.getTasks().subscribe((tasks) => {
            this.tasks = tasks;
        });
    }

    addTask() {
        this.todoService.createTask(this.taskInput.value!).subscribe((task) => {
            this.tasks.unshift(task);
        });
    }

    deleteTask(id: string) {
        this.tasks = this.tasks.filter((task) => task.id !== id);
    }

}
