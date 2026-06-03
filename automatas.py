import tkinter as tk
from tkinter import messagebox
import requests

ESP32_IP = "172.17.220.44"

BASE_URL = f"http://{ESP32_IP}"

USUARIOS = {
    "admin": "1234",
    "operador": "2026"
}


class VentanaLogin:

    def __init__(self, root):
        self.root = root
        self.root.title("Login")

        tk.Label(root, text="Usuario").pack(pady=5)

        self.txtUsuario = tk.Entry(root)
        self.txtUsuario.pack()

        tk.Label(root, text="Contraseña").pack(pady=5)

        self.txtPassword = tk.Entry(root, show="*")
        self.txtPassword.pack()

        tk.Button(
            root,
            text="Ingresar",
            command=self.login
        ).pack(pady=10)

    def login(self):

        usuario = self.txtUsuario.get().strip()
        password = self.txtPassword.get().strip()

        if usuario not in USUARIOS:
            messagebox.showerror(
                "Error",
                "Usuario incorrecto"
            )
            return

        if USUARIOS[usuario] != password:
            messagebox.showerror(
                "Error",
                "Contraseña incorrecta"
            )
            return

        try:

            respuesta = requests.get(
                f"{BASE_URL}/datos",
                timeout=3
            )

            if respuesta.status_code == 200:

                self.root.destroy()

                principal = tk.Tk()

                AplicacionPrincipal(
                    principal,
                    usuario
                )

                principal.mainloop()

            else:
                messagebox.showerror(
                    "Error",
                    "ESP32 no responde"
                )

        except Exception as e:

            messagebox.showerror(
                "Error",
                str(e)
            )


class AplicacionPrincipal:

    def __init__(self, root, usuario):

        self.root = root
        self.usuario = usuario

        self.root.title("AutoDrink")

        self.t1 = 100
        self.t2 = 100
        self.t3 = 100

        self.lblUsuario = tk.Label(
            root,
            text=f"Usuario: {usuario}"
        )

        self.lblUsuario.pack(pady=10)

        self.lblT1 = tk.Label(root)
        self.lblT2 = tk.Label(root)
        self.lblT3 = tk.Label(root)

        self.lblT1.pack()
        self.lblT2.pack()
        self.lblT3.pack()

        tk.Button(
            root,
            text="Dispensar T1",
            command=lambda: self.dispensar(1)
        ).pack(fill="x")

        tk.Button(
            root,
            text="Dispensar T2",
            command=lambda: self.dispensar(2)
        ).pack(fill="x")

        tk.Button(
            root,
            text="Dispensar T3",
            command=lambda: self.dispensar(3)
        ).pack(fill="x")

        tk.Button(
            root,
            text="Actualizar",
            command=self.leer_tanques
        ).pack(fill="x")

        tk.Button(
            root,
            text="Apagar Sistema",
            command=self.apagar
        ).pack(fill="x")

        self.leer_tanques()

    def actualizar_pantalla(self):

        self.lblT1.config(
            text=f"Tanque 1: {self.t1}%"
        )

        self.lblT2.config(
            text=f"Tanque 2: {self.t2}%"
        )

        self.lblT3.config(
            text=f"Tanque 3: {self.t3}%"
        )

    def leer_tanques(self):

        try:

            respuesta = requests.get(
                f"{BASE_URL}/tanques",
                timeout=3
            )

            datos = respuesta.text.strip()

            partes = datos.split(",")

            if len(partes) != 3:

                messagebox.showerror(
                    "Error",
                    "Formato inválido"
                )

                return

            self.t1 = int(partes[0])
            self.t2 = int(partes[1])
            self.t3 = int(partes[2])

            self.actualizar_pantalla()

        except Exception as e:

            messagebox.showerror(
                "Error",
                str(e)
            )

    def dispensar(self, tanque):

        try:

            respuesta = requests.get(
                f"{BASE_URL}/dispensar?tanque={tanque}",
                timeout=5
            )

            texto = respuesta.text.strip().upper()

            if texto == "OK" or texto == "1":

                messagebox.showinfo(
                    "Correcto",
                    f"Dispensado tanque {tanque}"
                )

                self.leer_tanques()

            else:

                messagebox.showwarning(
                    "Aviso",
                    texto
                )

        except Exception as e:

            messagebox.showerror(
                "Error",
                str(e)
            )

    def apagar(self):

        try:

            respuesta = requests.get(
                f"{BASE_URL}/control?estado=0",
                timeout=3
            )

            texto = respuesta.text.strip().upper()

            if texto == "OK" or texto == "0":

                messagebox.showinfo(
                    "Sistema",
                    "Sistema apagado"
                )

                self.root.destroy()

            else:

                messagebox.showerror(
                    "Error",
                    "No se pudo apagar"
                )

        except Exception as e:

            messagebox.showerror(
                "Error",
                str(e)
            )


if __name__ == "__main__":

    ventana = tk.Tk()

    VentanaLogin(ventana)

    ventana.mainloop()