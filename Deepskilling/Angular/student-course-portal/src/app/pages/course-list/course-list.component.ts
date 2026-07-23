import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Course } from '../../models/course.model';
import { CourseCardComponent } from '../../components/course-card/course-card.component';
import { loadCourses } from '../../store/course/course.actions';
import { selectAllCourses, selectCoursesLoading } from '../../store/course/course.selectors';
import { EnrollmentService } from '../../services/enrollment.service';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [CommonModule, CourseCardComponent],
  templateUrl: './course-list.component.html'
})
export class CourseListComponent implements OnInit {
  courses$: Observable<Course[]>;
  isLoading$: Observable<boolean>;
  selectedCourseId: number | null = null;

  constructor(
    private store: Store,
    private enrollmentService: EnrollmentService,
    public loadingService: LoadingService
  ) {
    this.courses$ = this.store.select(selectAllCourses);
    this.isLoading$ = this.store.select(selectCoursesLoading);
  }

  ngOnInit(): void {
    this.store.dispatch(loadCourses());
  }

  onEnroll(courseId: number) {
    if (this.enrollmentService.isEnrolled(courseId)) {
      this.enrollmentService.unAccess(courseId); -- typo check, make it unenroll
    } else {
      this.enrollmentService.enroll(courseId);
    }
    this.selectedCourseId = courseId;
  }

  // Fixing above unAccess typo
  onEnrollRequest(courseId: number) {
    if (this.enrollmentService.isEnrolled(courseId)) {
      this.enrollmentService.unenroll(courseId);
    } else {
      this.enrollmentService.enroll(courseId);
    }
    this.selectedCourseId = courseId;
  }

  trackByCourseId(index: number, course: Course): number {
    return course.id;
  }
}