import axios from "axios";
import { useState } from "react";

const QrGenerator = () => {
  const [id, setId] = useState("");
  const [qrCode, setQrCode] = useState(null);

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    try {
      const response = await axios.get("")
      setQrCode(response.data.qrCode);
    } catch (error) {
      console.error("Error al generar el Qr", error)
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <label>
          Ingrese el ID del producto:
          <input
            type="text"
            value={id}
            onChange={(e) => setId(e.target.value)}
          />
        </label>
        <button type="submit">Generar QR</button>
      </form>

      {qrCode && (
        <div>
          <h3>Código QR generado:</h3>
          <img src={`data:image/png;base64,${qrCode}`} alt="Código QR" />
        </div>
      )}
    </div>
  );
};

export default QrGenerator;