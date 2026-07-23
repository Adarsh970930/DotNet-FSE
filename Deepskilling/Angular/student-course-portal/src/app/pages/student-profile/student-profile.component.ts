import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnrollmentService } from '../../services/enrollment.service';
import { CourseService } from '../../services/course.service';
import { Course } from '../../models/course.model';

@Component({
  selector: 'app-student-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './student-profile.component.html'
})
export class StudentProfileComponent implements OnInit {
  enrolledCourses: Course[] = [];

  constructor(
    private enrollmentService: EnrollmentService,
    private courseService: CourseService
  ) {}

  ngOnInit(): void {
    this.courseService.getCourses().subscribe(courses => {
      this.enrolledCourses = this.enrollmentService.getEnrolledCourses(courses);
    });
  }
}