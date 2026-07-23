import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { CourseService } from '../../services/course.service';
import * as CourseActions from './course.actions';
import { catchError, map, mergeMap, of } from 'rxjs';

@Injectable()
export class CourseEffects {
  loadCourses$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CourseActions.loadCourses),
      mergeMap(() =>
        this.courseService.getCourses().pipe(
          map(courses => CourseActions.loadCoursesSuccess({ courses })),
          catchError(error => of(CourseActions.loadCoursesFailure({ error: error.message })))
        )
      )
    )
  );

  constructor(private actions$: Actions, private courseService: CourseService) {}
}