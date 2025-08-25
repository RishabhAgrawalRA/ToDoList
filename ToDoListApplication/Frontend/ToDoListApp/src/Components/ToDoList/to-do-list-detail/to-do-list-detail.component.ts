import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToDoListService } from '../../../services/to-do-list.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UpdateToDoList } from '../../../models/UpdateToDoList.model';
import { CommonModule } from '@angular/common';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-to-do-list-detail',
  imports: [CommonModule, ReactiveFormsModule, MatSnackBarModule],
  templateUrl: './to-do-list-detail.component.html',
  styleUrl: './to-do-list-detail.component.css'
})
export class ToDoListDetailComponent implements OnInit {

  id!:number
  toDoListForm!:FormGroup
  updatedToDoList!: UpdateToDoList

  constructor(private route:ActivatedRoute, private _service:ToDoListService, private _fb:FormBuilder, private router: Router, private snackBar: MatSnackBar){

  }

  get title(){
      return this.toDoListForm.controls['title'];
    }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    this.toDoListForm = this._fb.group({
      id: [{value:'', disabled:true}],
      title: ['', [Validators.required, Validators.minLength(5)]],
      description: [''],
      isCompleted: ['']
    });

    this._service.getToDoListByID(this.id).subscribe({
      next : response => this.toDoListForm.patchValue(response) ,
      error : err => console.log(err)
    });
  }

  onSubmit(){

    this.updatedToDoList = {
      title:this.toDoListForm.get('title')?.value,
      description:this.toDoListForm.get('description')?.value ?? '',
      isCompleted:this.toDoListForm.get('isCompleted')?.value,
      createdAt: new Date()
    }

    this._service.updateToDoList(this.id, this.updatedToDoList).subscribe({
      next : response => {
          this.snackBar.open('To-do updated successfully!', 'Close', {
              duration: 2000
            });
            this.router.navigateByUrl('/todolist')
      },
      error : err => console.log(err)
    });
  }
 
  DeleteToDoList(){
    var con = confirm("Are you sure want to Delete");
    console.log(con);
    if(con){
        this._service.deleteToDoList(this.id).subscribe({
        next : response => {
          this.snackBar.open('To-do deleted successfully!', 'Close', {
              duration: 2000
            });
            this.router.navigateByUrl('/todolist')
      },
        error : err => console.log(err)
      });
    }
  }

}
