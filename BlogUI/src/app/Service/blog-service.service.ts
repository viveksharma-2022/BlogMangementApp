import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBlogs } from '../Models/blogs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BlogServiceService {

  private apiUrl = 'https://localhost:7063/api/Blog'; 
  constructor(private http:HttpClient) {

   }

  getBlogs(): Observable<IBlogs[]> {
    return this.http.get<IBlogs[]>(this.apiUrl);
  }
  createBlog(Blog: IBlogs[]): Observable<any> {
    console.log('Creating Blog:', Blog);
    return this.http.post(this.apiUrl, Blog);
  }

  getBlogById(BlogId: number): Observable<IBlogs> {
    const url = `${this.apiUrl}/${BlogId}`;
    return this.http.get<IBlogs>(url);
  }

  updateBlog(BlogId: number, Blog: IBlogs): Observable<IBlogs> {
    const url = `${this.apiUrl}/${BlogId}`;
    return this.http.put<IBlogs>(url, Blog);
  }

  deleteBlog(BlogId: number): Observable<any> {
    const url = `${this.apiUrl}/${BlogId}`;
    return this.http.delete(url);
  }
}
