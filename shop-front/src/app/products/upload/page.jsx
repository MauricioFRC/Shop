"use client";
import { useState } from 'react'
import { FileInput, Label } from "flowbite-react";

export default function Upload() {
  const [file, setFile] = useState(null)

  function handleFileChange(event) {
    setFile(event.target.files[0])
  }

  return (
    <div>
      <form action="/api/upload" method="post" encType="multipart/form-data">
        <input type="file" name="image" id="image" />
        <button type="submit" className="border">Upload</button>
      </form>
    </div>
  );
}