// import {
//   beforeEach,
//   beforeEachProviders,
//   describe,
//   expect,
//   it,
//   inject,
// } from '@angular/core/testing';
// import { ComponentFixture, TestComponentBuilder } from '@angular/compiler/testing';
// import { Component } from '@angular/core';
// import { By } from '@angular/platform-browser';
// import { PrayerTimesComponent } from './prayer-times.component';

// describe('Component: PrayerTimes', () => {
//   let builder: TestComponentBuilder;

//   beforeEachProviders(() => [PrayerTimesComponent]);
//   beforeEach(inject([TestComponentBuilder], function (tcb: TestComponentBuilder) {
//     builder = tcb;
//   }));

//   it('should inject the component', inject([PrayerTimesComponent],
//       (component: PrayerTimesComponent) => {
//     expect(component).toBeTruthy();
//   }));

//   it('should create the component', inject([], () => {
//     return builder.createAsync(PrayerTimesComponentTestController)
//       .then((fixture: ComponentFixture<any>) => {
//         let query = fixture.debugElement.query(By.directive(PrayerTimesComponent));
//         expect(query).toBeTruthy();
//         expect(query.componentInstance).toBeTruthy();
//       });
//   }));
// });

// @Component({
//   selector: 'test',
//   template: `
//     <app-prayer-times></app-prayer-times>
//   `,
//   directives: [PrayerTimesComponent]
// })
// class PrayerTimesComponentTestController {
// }

