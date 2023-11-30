function hahaHehe(filename, data, huhu) {
    console.log(huhu);

    // Create the URL
    const file = new File([data], filename);
    const exportUrl = URL.createObjectURL(file);

    // Create the <a> element and click on it
    const a = document.createElement("a");
    document.body.appendChild(a);
    a.href = exportUrl;
    a.download = filename;
    a.target = "_self";
    a.click();

    URL.revokeObjectURL(exportUrl);
}
