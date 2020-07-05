let coursesByYear = [];

function updateCategories(selectedCourse) {
    let year = $("#year-input").val();
    let course = coursesByYear[parseInt(year) - 1].find(el => el.courseId === selectedCourse);
    if (course === undefined) {
        $("#category-input").empty();
        return;
    }
    let opts = [new Option("Izaberite...", "", true, true)];
    opts = opts.concat(course.categories.map(cat => new Option(cat.name, cat.categoryId.toString(), false, false)));
    $("#category-input").empty().append(opts);
}

$("#course-input")
    .autocomplete({
        minLength: 0,
        select: (event, ui) => {
            $("#course-input").attr("data-selected-course-id", ui.item.courseId);
            updateCategories(ui.item.courseId);
        },
        search: (event, ui) => {
            $("#category-input").empty();
        }
    })
    .autocomplete("disable")
    .focus(ev => {
        if ($("#year-input").val() !== "") {
            $("#course-input").autocomplete("search", "");
        }
    });

$("#year-input").change(ev => {
    let year = $("#year-input").val();

    if (year === "") {
        $("#course-input")
            .autocomplete("disable")
            .val("")
            .attr("data-selected-course-id", null);

        $("#category-input").empty();
        return;
    }

    let courseInput = $("#course-input");
    courseInput.autocomplete("option", "source", coursesByYear[parseInt(year) - 1]);
    courseInput.autocomplete("enable");
});

for (let i = 0; i < 4; ++i) {
    $.ajax({
        url: `/api/StudyMaterialApi/courses/${i}`,
        method: "GET",
        success: response => {
            coursesByYear[i] = response.map(course => { course.value = course.name; return course });
            if (coursesByYear[0] && coursesByYear[1] && coursesByYear[2] && coursesByYear[3]) {
                $(".spinner-wrapper").hide();
                console.log("Spinner hidden");
            }
        },
        dataType: "json"
    });
}



