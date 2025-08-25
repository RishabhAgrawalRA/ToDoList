import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDoList } from '../models/ToDoList.model';
import { UpdateToDoList } from '../models/UpdateToDoList.model';
import { AddToDoList } from '../models/AddToDoList.model';

@Injectable({
  providedIn: 'root'
})

export class ToDoListService {

  private url:string = "https://localhost:7008/api/";
  constructor(private _http:HttpClient) { }

  getToDoList():Observable<ToDoList[]>{
    return this._http.get<ToDoList[]>(this.url+'todolist');
  }

  getToDoListByID(id:number):Observable<ToDoList>{
    return this._http.get<ToDoList>(`${this.url}todolist/${id}`);
  }
  
  updateIsComplete(id:number, isChecked:boolean):Observable<ToDoList>{
    return this._http.patch<ToDoList>(`${this.url}todolist/${id}/isCompleted`,{isCompleted:isChecked});
  }

  addToDoList(todolist:AddToDoList):Observable<ToDoList>{
    return this._http.post<ToDoList>(`${this.url}todolist`, todolist);
  }

  updateToDoList(id:number, toDoList:UpdateToDoList):Observable<UpdateToDoList>{
    return this._http.put<UpdateToDoList>(`${this.url}todolist/${id}`, toDoList);
  }

  deleteToDoList(id:number):Observable<void>{
    return this._http.delete<void>(`${this.url}todolist/${id}`);
  }

  getToDoListByDate(date:Date):Observable<ToDoList[]>{
    let dateString = date.toISOString().split('T')[0];
    dateString+="T00:00:00";
    return this._http.get<ToDoList[]>(`${this.url}todolist/${dateString}`);
  }

  getToDoListByMonth(month:number):Observable<ToDoList[]>{
    return this._http.get<ToDoList[]>(`${this.url}todolist/month/${month}`);
  }
}
