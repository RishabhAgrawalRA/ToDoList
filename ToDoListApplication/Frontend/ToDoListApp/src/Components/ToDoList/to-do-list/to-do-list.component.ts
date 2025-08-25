import { Component, OnInit } from '@angular/core';
import { ToDoListService } from '../../../services/to-do-list.service';
import { ToDoList } from '../../../models/ToDoList.model';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-to-do-list',
  imports: [CommonModule, RouterModule, MatSnackBarModule],
  templateUrl: './to-do-list.component.html',
  styleUrl: './to-do-list.component.css'
})

export class ToDoListComponent implements OnInit{

  toDoList? : ToDoList[];
  constructor(private _service:ToDoListService, private router:Router, private snackBar: MatSnackBar) {
  }


  ngOnInit(): void {
    this._service.getToDoList().subscribe({
      next : response => this.toDoList=response,
      error : err  => console.log(err)      
    });
  }

  updateIsComplete(id:number|undefined, isChecked:boolean){
    if(id)
    { 
      this._service.updateIsComplete(id,isChecked).subscribe({
        next : response => {
          this.toDoList = this.toDoList?.map(item => item.id===response.id ? {...item, isCompleted:  response.isCompleted}:item);
          this.router.navigateByUrl('/todolist');
          if(isChecked){
              this.snackBar.open('Yes task completed!', 'Close', {
              duration: 2000
            });
          }
          else{
              this.snackBar.open('Some Work To Do!', 'Close', {
              duration: 2000
            });
          }
        },
        error : err => console.log(err)
      });
    }
  }
}
