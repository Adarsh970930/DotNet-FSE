import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators, AbstractControl, ValidationErrors, ReactiveFormsModule } from '@angular/forms';
import { CanComponentDeactivate } from '../../guards/unsaved-changes.guard';

@Component({
  selector: 'app-reactive-enrollment-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './reactive-enrollment-form.component.html'
})
export class ReactiveEnrollmentFormComponent implements OnInit, CanComponentDeactivate {
  enrollForm!: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.enrollForm = this.fb.group({
      studentName: ['', [Validators.required, Validators.minLength(3)]],
      studentEmail: ['', [Validators.required, Validators.email], [this.simulateEmailCheck]],
      courseId: [null, [Validators.required, this.noCourseCode]],
      preferredSemester: ['Odd', Validators.required],
      agreeToTerms: [false, Validators.requiredTrue],
      additionalCourses: this.fb.array([])
    });
  }

  // Custom Synchronous Validator
  noCourseCode(control: AbstractControl): ValidationErrors | null {
    const value = String(control.value || '');
    if (value.startsWith('XX')) {
      return { noCourseCode: true };
    }
    return null;
  }

  // Custom Asynchronous Validator
  simulateEmailCheck(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve) => {
      setTimeout(() => {
        const val = String(control.value || '');
        if (val.includes('test@')) {
          resolve({ emailTaken: true });
        } else {
          resolve(null);
        }
      }, 800);
    });
  }

  get additionalCourses(): FormArray {
    return this.enrollForm.get('additionalCourses') as FormArray;
  }

  addCourse() {
    this.additionalCourses.push(new FormControl('', Validators.required));
  }

  removeCourse(index: number) {
    this.additionalCourses.removeAt(index);
  }

  onSubmit() {
    if (this.enrollForm.valid) {
      console.log('Form values (value):', this.enrollForm.value);
      console.log('Form values (getRawValue):', this.enrollForm.getRawValue());
      this.submitted = true;
      this.enrollForm.markAsPristine();
    }
  }

  canDeactivate(): boolean {
    if (this.enrollForm.dirty && !this.submitted) {
      return window.confirm('You have unsaved changes. Leave?');
    }
    return true;
  }
}