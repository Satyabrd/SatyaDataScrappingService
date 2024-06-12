<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" indent="yes" />
    <xsl:template match="/">
        <html>
            <head>
                <title>Transformed Data</title>
            </head>
            <body>
                <h2>Transformed Headlines</h2>
                <xsl:apply-templates/>
            </body>
        </html>
    </xsl:template>

    <xsl:template match="h1">
        <div>
            <h1><xsl:value-of select="."/></h1>
        </div>
    </xsl:template>
</xsl:stylesheet>
