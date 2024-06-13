<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="html" indent="yes"/>
    
    <!-- Template to match the root element -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Vehicle Data</title>
            </head>
            <body>
                <h2>Vehicle Listings</h2>
                <table border="1">
                    <tr>
                        <th>Title</th>
                        <th>Price</th>
                        <th>Media</th>
                        <th>Footer</th>
                    </tr>
                    <xsl:apply-templates select="//li[contains(@class, 'vehicle-card')]"/>
                </table>
            </body>
        </html>
    </xsl:template>

    <!-- Template to match each vehicle card -->
    <xsl:template match="li[contains(@class, 'vehicle-card')]">
        <tr>
            <td><xsl:value-of select=".//span[@data-testid='title-skeleton']"/></td>
            <td><xsl:value-of select=".//span[@data-testid='price-skeleton']"/></td>
            <td><xsl:value-of select=".//span[@data-testid='media-skeleton']"/></td>
            <td><xsl:value-of select=".//span[@data-testid='footer-skeleton']"/></td>
        </tr>
    </xsl:template>
</xsl:stylesheet>
