import { Component, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreditLabelPipe } from '../../pipes/credit-label.pipe';
import { HighlightDirective } from '../../directives/highlight.directive';
import { EnrollmentService } from '../../services/enrollment.service';
import { Course } from '../../models/course.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-course-card',
  standalone: true,
  imports: [CommonModule, CreditLabelPipe, HighlightDirective],
  templateUrl: './course-card.component.html',
  styleUrls: ['./course-card.component.css']
})
export class CourseCardComponent implements OnChanges {
  @Input() course!: Course;
  @Output() enrollRequested = new EventEmitter<number>();
  isExpanded = false;

  constructor(public enrollmentService: EnrollmentService, private router: Router) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['course']) {
      console.log('Course changed:', changes['course'].previousValue, 'to', changes['course'].currentValue);
    }
  }

  get cardClasses() {
    return {
      'card--enrolled': this.enrollmentService.isEnrolled(this.course.id),
      'card--full': this.course.credits >= 4
    };
  }

  toggleExpand() {
    this.isExpanded = !this.isExpanded;
  }

  viewDetails() {
    this.router.navigate(['/courses', this.course.id]);
  }
}