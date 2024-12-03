import { Component, ElementRef, viewChild, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BlogServiceService } from '../Service/blog-service.service';
import { IBlogs } from '../Models/blogs';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrl: './blogs.component.css'
})
export class BlogsComponent {
  @ViewChild('myModal') model: ElementRef | undefined;

  blogForm:FormGroup= new FormGroup({});
  pages: number = 1;
  searchedKeyword :any ='';
  bloglist:IBlogs[]=[];
  submitted=false;

  constructor(private blogService: BlogServiceService, private fb:FormBuilder) { }

  ngOnInit(): void {
    this.setFormState();
    this.getBlogs();
  }

  openModal()
  {
    const blogModal= document.getElementById('myModal');
    if(blogModal!=null)
    {
      blogModal.style.display='block';
    }
  }

  closeModal()
  {
    this.setFormState();
    if(this.model!=null)
    {
      this.model.nativeElement.style.display='none';
    }

  }

  getBlogs(){
    this.blogService.getBlogs().subscribe((res)=>{
      this.bloglist=res;
    })
  }
  get blogdata(): { [key: string]: AbstractControl } {
    return this.blogForm.controls;
  }

  setFormState(){
    this.blogForm= this.fb.group({
      id:[0],
      text:['', [Validators.required]],
      userName:['', [Validators.required]],
      
    })
  }

  formValues:any;
  onSubmit()
  {
    this.submitted = true;
    if(this.blogForm.invalid){
      alert('Please Fill All Fields!')
      return;
    }
    if(this.blogForm.value.id==0)
    {
      this.formValues=this.blogForm.value;
      this.blogService.createBlog(this.formValues).subscribe((res)=>{
      alert('Blog Added Successfully!');
      this.getBlogs();
      this.blogForm.reset();
      this.closeModal();

    });
    }
    else{
      this.formValues=this.blogForm.value;
      this.blogService.updateBlog(this.formValues.id,this.formValues).subscribe((res)=>{
      alert('Blog Updated Successfully!');
      this.getBlogs();
      this.blogForm.reset();
      this.closeModal();

    });
    }
    
  }
  onEdit(blog :IBlogs){
    this.openModal();
    this.blogForm.patchValue(blog);
  }
  onDelete(id:number){
    const isConfirm=confirm('Are you Sure You Want To Delete?');
    if(isConfirm)
    {
      this.blogService.deleteBlog(id).subscribe((res)=>{
        alert("Blog Deleted Successfully!");
        this.getBlogs();
      })
    }
  }

}
