window.tpi = {
    graficos: {},

    dibujarGraficoBarras: function (canvasId, titulo, etiquetas, series) {
        window.tpi.destruirGrafico(canvasId);

        const canvas = document.getElementById(canvasId);
        if (!canvas) {
            return;
        }

        const moneda = new Intl.NumberFormat('es-AR', { style: 'currency', currency: 'ARS', maximumFractionDigits: 0 });

        window.tpi.graficos[canvasId] = new Chart(canvas, {
            type: 'bar',
            data: {
                labels: etiquetas,
                datasets: series.map(s => ({
                    label: s.nombre,
                    data: s.valores,
                    backgroundColor: s.color,
                    borderRadius: 3
                }))
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    title: { display: true, text: titulo, font: { size: 15 } },
                    legend: { position: 'bottom' },
                    tooltip: {
                        callbacks: {
                            label: contexto => `${contexto.dataset.label}: ${moneda.format(contexto.parsed.y)}`
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: { callback: valor => moneda.format(valor) }
                    }
                }
            }
        });
    },

    destruirGrafico: function (canvasId) {
        const anterior = window.tpi.graficos[canvasId];
        if (anterior) {
            anterior.destroy();
            delete window.tpi.graficos[canvasId];
        }
    },

    descargarArchivo: async function (nombre, contenido, tipo) {
        const buffer = await contenido.arrayBuffer();
        const url = URL.createObjectURL(new Blob([buffer], { type: tipo }));
        const enlace = document.createElement('a');
        enlace.href = url;
        enlace.download = nombre;
        document.body.appendChild(enlace);
        enlace.click();
        enlace.remove();
        URL.revokeObjectURL(url);
    }
};
