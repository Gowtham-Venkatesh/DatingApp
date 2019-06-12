import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable()

export class PreventUnsavedChanges implements CanDeactivate<MemberEditComponent> {
    canDeactivate(component: MemberEditComponent) {
        if (component.editform.dirty) {
            return confirm('Are You Sure You Want to Continue? Any Unsaved Changes will be lost');
        }
        return true;
    }
}
