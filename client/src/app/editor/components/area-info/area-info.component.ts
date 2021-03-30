import { Component, Input, Output, EventEmitter, OnChanges } from "@angular/core";
import { AreaInfo } from "../../models/area-info.model";

@Component({
    selector: "app-area-info",
    templateUrl: "./area-info.component.html",
})
export class AreaInfoComponent implements OnChanges
{
    @Input() info: AreaInfo;

    @Output() vnumRangeChangedEvent = new EventEmitter();

    constructor() { }

    ngOnChanges(): void
    {
        this.onVnumRangeChanged();
    }

    onVnumRangeChanged(): void
    {
        this.vnumRangeChangedEvent.emit();
    }
}
