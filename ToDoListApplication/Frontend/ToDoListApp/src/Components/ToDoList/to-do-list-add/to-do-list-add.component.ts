import { Component, OnInit } from '@angular/core';
import {CommonModule } from '@angular/common'
import {ReactiveFormsModule, FormBuilder, FormGroup, Validator, Validators } from '@angular/forms'
import { ToDoListService } from '../../../services/to-do-list.service';
import { AddToDoList } from '../../../models/AddToDoList.model';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-to-do-list-add',
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './to-do-list-add.component.html',
  styleUrl: './to-do-list-add.component.css'
})
export class ToDoListAddComponent implements OnInit {

    addToDoListForm!:FormGroup;
    id!:number;
    addToDoList!: AddToDoList;


    constructor(private fb:FormBuilder, private _service:ToDoListService, private router:Router, private snackBar: MatSnackBar){

    }

    get title(){
      return this.addToDoListForm.controls['title'];
    }

    ngOnInit(): void {
      this.addToDoListForm = this.fb.group({
        title:['', [Validators.required, Validators.minLength(5)]],
        description:[''],
        isCompleted:[false],
      });
      
      //this.findMaxId();  not needed now DB take care
    }

    onSubmit(){
      this.addToDoList={
          title:this.addToDoListForm.get('title')?.value,
          description:this.addToDoListForm.get('description')?.value ?? '',
          isCompleted:this.addToDoListForm.get('isCompleted')?.value,
          createdAt: new Date()
        };

        this._service.addToDoList(this.addToDoList).subscribe({
          next : response => {
            this.snackBar.open('To-do added successfully!', 'Close', {
            duration: 2000
          });
            this.router.navigateByUrl('/todolist')
          },
          error : err => console.log(err)
        });
    }

    /*
    findMaxId(){
      this._service.getToDoList().subscribe({
        next : data => this.id = Math.max(...data.map(x => x.id))+1,
        error : err => console.log(err)
      });
    }
    */
}
