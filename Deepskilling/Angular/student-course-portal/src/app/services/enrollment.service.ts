import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, map, switchMap } from 'rxjs';
import { Course } from '../models/course.model';
import { CourseService } from './course.service';

@Injectable({
  providedIn: 'root'
})
export class EnrollmentService {
  private enrolledSubject = new BehaviorSubject<number[]>([]);
  enrolledCourseIds$ = this.enrolledSubject.asObservable();

  constructor(private courseService: CourseService) {}

  enroll(courseId: number) {
    const current = this.enrolledSubject.value;
    if (!current.includes(courseId)) {
      this.enrolledSubject.next([...current, courseId]);
    }
  }

  unenroll(courseId: number) {
    this.enrolledSubject.next(this.enrolledSubject.value.filter(id => id !== courseId));
  }

  isEnrolled(courseId: number): boolean {
    return this.enrolledSubject.value.includes(courseId);
  }

  getEnrolledCourses(allCourses: Course[]): Course[] {
    return allCourses.filter(c => this.enrolledSubject.value.includes(c.id));
  }
}